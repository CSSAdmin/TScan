using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace TwainLib
{
public enum TwainCommand
	{
	Not				= -1,
	Null			= 0,
	TransferReady	= 1,
	CloseRequest	= 2,
	CloseOk			= 3,
	DeviceEvent		= 4
	}




public class Twain
	{
	private const short CountryUSA		= 1;
	private const short LanguageUSA		= 13;
    private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

	public Twain()
		{
		appid = new TwIdentity();
		appid.Id				= IntPtr.Zero;
		appid.Version.MajorNum	= 1;
		appid.Version.MinorNum	= 1;
		appid.Version.Language	= LanguageUSA;
		appid.Version.Country	= CountryUSA;
		appid.Version.Info		= "Hack 1";
		appid.ProtocolMajor		= TwProtocol.Major;
		appid.ProtocolMinor		= TwProtocol.Minor;
		appid.SupportedGroups	= (int)(TwDG.Image | TwDG.Control);
		appid.Manufacturer		= "NETMaster";
		appid.ProductFamily		= "Freeware";
		appid.ProductName		= "Hack";

		srcds = new TwIdentity();
		srcds.Id = IntPtr.Zero;

		evtmsg.EventPtr = Marshal.AllocHGlobal( Marshal.SizeOf( winmsg ) );
		}

	~Twain()
		{
		Marshal.FreeHGlobal( evtmsg.EventPtr );
		}




	public void Init( IntPtr hwndp )
		{
		Finish();
		TwRC rc = DSMparent( appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp );
		if( rc == TwRC.Success )
			{
			rc = DSMident( appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds );
			if( rc == TwRC.Success )
				hwnd = hwndp;
			else
               
				rc = DSMparent( appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp );
			}
		}

	public void Select()
		{
		TwRC rc;
		CloseSrc();
		if( appid.Id == IntPtr.Zero )
			{
			Init( hwnd );
			if( appid.Id == IntPtr.Zero )
				return;
			}
		rc = DSMident( appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, srcds );
		}


	public void Acquire()
		{
		TwRC rc;
		CloseSrc();
		if( appid.Id == IntPtr.Zero )
			{
			Init( hwnd );
			if( appid.Id == IntPtr.Zero )
                MessageBox.Show("Scanner not found.","Scan Interface", MessageBoxButtons.OK, MessageBoxIcon.Information); 
				return;
			}
            TwIdentity srcds1 = new TwIdentity();
		rc = DSMident( appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds );
        if (rc != TwRC.Success)
            
			return;

		//TwCapability cap = new TwCapability( TwCap.XferCount, 1 );
        //changed XferCount to -i for ADF multipage scanning
        TwCapability cap = new TwCapability(TwCap.XferCount, -1);
		rc = DScap( appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap );
		if( rc != TwRC.Success )
			{
			CloseSrc();
			return;
			}

            TwUserInterface guif = new TwUserInterface();
            guif.ShowUI = 1;
           guif.ModalUI = 1;
            guif.ParentHand = hwnd;
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                return;
            }
		}


	public ArrayList TransferPictures()
		{
		ArrayList pics = new ArrayList();
		if( srcds.Id == IntPtr.Zero )
			return pics;

		TwRC rc;
		IntPtr hbitmap = IntPtr.Zero;
		TwPendingXfers pxfr = new TwPendingXfers();
		
		do
			{
			pxfr.Count = 0;
			hbitmap = IntPtr.Zero;

			TwImageInfo	iinf = new TwImageInfo();
			rc = DSiinf( appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf );
			if( rc != TwRC.Success )
				{
				CloseSrc();
				return pics;
				}

			rc = DSixfer( appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap );
			if( rc != TwRC.XferDone )
				{
				CloseSrc();
				return pics;
				}

			rc = DSpxfer( appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr );
			if( rc != TwRC.Success )
				{
				CloseSrc();
				return pics;
				}

			pics.Add( hbitmap );
			}
		while( pxfr.Count != 0 );

		rc = DSpxfer( appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr );
		return pics;
		}


	public TwainCommand PassMessage( ref Message m )
		{
		if( srcds.Id == IntPtr.Zero )
			return TwainCommand.Not;

		int pos = GetMessagePos();

		winmsg.hwnd		= m.HWnd;
		winmsg.message	= m.Msg;
		winmsg.wParam	= m.WParam;
		winmsg.lParam	= m.LParam;
		winmsg.time		= GetMessageTime();
		winmsg.x		= (short) pos;
		winmsg.y		= (short) (pos >> 16);
		
		Marshal.StructureToPtr( winmsg, evtmsg.EventPtr, false );
		evtmsg.Message = 0;
		TwRC rc = DSevent( appid, srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref evtmsg );
		if( rc == TwRC.NotDSEvent )
			return TwainCommand.Not;
		if( evtmsg.Message == (short) TwMSG.XFerReady )
			return TwainCommand.TransferReady;
		if( evtmsg.Message == (short) TwMSG.CloseDSReq )
			return TwainCommand.CloseRequest;
		if( evtmsg.Message == (short) TwMSG.CloseDSOK )
			return TwainCommand.CloseOk;
		if( evtmsg.Message == (short) TwMSG.DeviceEvent )
			return TwainCommand.DeviceEvent;
        if (rc == TwRC.Failure)
            return TwainCommand.Not;
		return TwainCommand.Null;
		}

	public void CloseSrc()
		{
		TwRC rc;
		if( srcds.Id != IntPtr.Zero )
			{
			TwUserInterface	guif = new TwUserInterface();
			rc = DSuserif( appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif );
			rc = DSMident( appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds );
			}
		}

	public void Finish()
		{
		TwRC rc;
		CloseSrc();
		if( appid.Id != IntPtr.Zero )
			rc = DSMparent( appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwnd );
		appid.Id = IntPtr.Zero;
		}

	private IntPtr		hwnd;
	private TwIdentity	appid;
	private TwIdentity	srcds;
	private TwEvent		evtmsg;
	private WINMSG		winmsg;
	


	// ------ DSM entry point DAT_ variants:
		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSMparent( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr refptr );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSMident( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity idds );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSMstatus( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat );


	// ------ DSM entry point DAT_ variants to DS:
		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSuserif( [In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface guif );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSevent( [In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent evt );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSstatus( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DScap( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability capa );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSiinf( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imginf );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSixfer( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap );

		[DllImport("twain_32.dll", EntryPoint="#1")]
	private static extern TwRC DSpxfer( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pxfr );


		[DllImport("kernel32.dll", ExactSpelling=true)]
	internal static extern IntPtr GlobalAlloc( int flags, int size );
		[DllImport("kernel32.dll", ExactSpelling=true)]
	internal static extern IntPtr GlobalLock( IntPtr handle );
		[DllImport("kernel32.dll", ExactSpelling=true)]
	internal static extern bool GlobalUnlock( IntPtr handle );
		[DllImport("kernel32.dll", ExactSpelling=true)]
	internal static extern IntPtr GlobalFree( IntPtr handle );

		[DllImport("user32.dll", ExactSpelling=true)]
	private static extern int GetMessagePos();
		[DllImport("user32.dll", ExactSpelling=true)]
	private static extern int GetMessageTime();


		[DllImport("gdi32.dll", ExactSpelling=true)]
	private static extern int GetDeviceCaps( IntPtr hDC, int nIndex );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
	private static extern IntPtr CreateDC( string szdriver, string szdevice, string szoutput, IntPtr devmode );

		[DllImport("gdi32.dll", ExactSpelling=true)]
	private static extern bool DeleteDC( IntPtr hdc );




	public static int ScreenBitDepth {
		get {
			IntPtr screenDC = CreateDC( "DISPLAY", null, null, IntPtr.Zero );
			int bitDepth = GetDeviceCaps( screenDC, 12 );
			bitDepth *= GetDeviceCaps( screenDC, 14 );
			DeleteDC( screenDC );
			return bitDepth;
			}
		}


		[StructLayout(LayoutKind.Sequential, Pack=4)]
	internal struct WINMSG
		{
		public IntPtr		hwnd;
		public int			message;
		public IntPtr		wParam;
		public IntPtr		lParam;
		public int			time;
		public int			x;
		public int			y;
    }

    
    #region For Saving Document

    private static bool GetCodecClsid(string filename, out Guid clsid)
    {
        clsid = Guid.Empty;
        string ext = Path.GetExtension(filename);
        if (ext == null)
            return false;
        ext = "*" + ext.ToUpper();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FilenameExtension.IndexOf(ext) >= 0)
            {
                clsid = codec.Clsid;
                return true;
            }
        }
        return false;
    }
    

    public  bool SaveDIBAs(IntPtr bminfo, IntPtr pixdat,int i)
    {
        Guid clsid;

        DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");

        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
        }

        string outFile = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "temp_scanned_page_" + DateTime.Now.Ticks;


       // if (!GetCodecClsid(@"c:\tiff\ScanPicture" + i + ".tif", out clsid))
        if (!GetCodecClsid(@outFile + i + ".tiff", out clsid))
        {

            MessageBox.Show("Unknown picture format for extension " + Path.GetExtension(@outFile),

                            "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return false;
        }

        IntPtr img = IntPtr.Zero;

        int st = GdipCreateBitmapFromGdiDib(bminfo, pixdat, ref img);

        if ((st != 0) || (img == IntPtr.Zero))

            return false;


        st = GdipSaveImageToFile(img, @outFile + i + ".tiff", ref clsid, IntPtr.Zero);

        GdipDisposeImage(img);

        return st == 0;
    }
       

    [DllImport("gdiplus.dll", ExactSpelling = true)]
    internal static extern int GdipCreateBitmapFromGdiDib(IntPtr bminfo, IntPtr pixdat, ref IntPtr image);

    [DllImport("gdiplus.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    internal static extern int GdipSaveImageToFile(IntPtr image, string filename, [In] ref Guid clsid, IntPtr encparams);

    [DllImport("gdiplus.dll", ExactSpelling = true)]
    internal static extern int GdipDisposeImage(IntPtr image);

#endregion

    
    /// <summary>
    /// Joins the tiff images.
    /// </summary>
    /// <param name="imageFiles">The image files.</param>
    /// <param name="outFile">The out file.</param>
    /// <param name="compressEncoder">The compress encoder.</param>
    public void JoinTiffImages(string[] imageFiles, string outFile, EncoderValue compressEncoder)
    {
        try
        {
            //If only one page in the collection, copy it directly to the target file.
            if (imageFiles.Length == 1)
            {
                File.Copy(imageFiles[0], outFile, true);
                return;
            }

            //use the save encoder
            Encoder enc = Encoder.SaveFlag;

            // EncoderParameters ep=new EncoderParameters(2);
            EncoderParameters ep = new EncoderParameters(1);
            ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);
            //ep.Param[1] = new EncoderParameter(Encoder.Compression,(long)compressEncoder); 

            Bitmap pages = null;
            int frame = 0;
            ImageCodecInfo info = GetEncoderInfo("image/tiff");

            foreach (ImageCodecInfo ice in ImageCodecInfo.GetImageDecoders())
            {
                if (ice.MimeType == "image/tiff")
                {
                    info = ice;
                }
            }
            foreach (string strImageFile in imageFiles)
            {
                if (frame == 0)
                {
                    pages = (Bitmap)Image.FromFile(strImageFile);

                    //save the first frame
                    pages.Save(outFile, info, ep);
                }
                else
                {
                    //save the intermediate frames
                    ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);

                    Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                    pages.SaveAdd(bm, ep);
                  //  bm.Dispose();  
                }

                if (frame == imageFiles.Length - 1)
                {
                    //flush and close.
                    ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
                    pages.SaveAdd(ep);
                }

                frame++;
            }
            //int gen = GC.GetGeneration(pages);
            //GC.Collect(gen);
        }
        catch (OutOfMemoryException oofmexp)
        {
            ////throw new OutOfMemoryException("An erro has Occured. Please refer to the Event Log");
            MessageBox.Show("please restart the application","Terrascan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
       
        return;
    }
    
    /// <summary>
    /// Getting the supported codec info.
    /// </summary>
    /// <param name="mimeType">description of mime type</param>
    /// <returns>image codec info</returns>
    private ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
        for (int j = 0; j < encoders.Length; j++)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }

        throw new Exception(mimeType + " mime type not found in ImageCodecInfo");
    }

    
	} // class Twain
}
