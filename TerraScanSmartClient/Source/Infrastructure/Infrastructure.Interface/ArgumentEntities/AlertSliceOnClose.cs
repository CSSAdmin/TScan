using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// this argument will be passed only when 
    /// the master form closed
    /// </summary>
    public class AlertSliceOnClose
    {
        /// <summary>
        /// the formno to identify the masterform
        /// </summary>
        private int formNo;

        /// <summary>
        /// flagto identify form close
        /// true to check page status
        /// false to stop close
        /// </summary>
        private bool flagFormClose;

        /// <summary>
        /// flagto identify for query engine open
        /// </summary>
        private bool flagForQueryEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AlertSliceOnClose"/> class.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        public AlertSliceOnClose(int formNo)
        {
            this.formNo = formNo;
            this.flagFormClose = true;
        }

        /// <summary>
        /// Gets or sets the form no.
        /// </summary>
        /// <value>The form no.</value>
        public int FormNo
        {
            get { return this.formNo; }
            set { this.formNo = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [flag form close].
        /// </summary>
        /// <value><c>true</c> if [flag form close]; otherwise, <c>false</c>.</value>
        public bool FlagFormClose
        {
            get { return this.flagFormClose; }
            set { this.flagFormClose = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [flag for query engine].
        /// </summary>
        /// <value><c>true</c> if [flag for query engine]; otherwise, <c>false</c>.</value>
        public bool FlagForQueryEngine
        {
            get { return this.flagForQueryEngine; }
            set { this.flagForQueryEngine = value; }
        }
    }
}
