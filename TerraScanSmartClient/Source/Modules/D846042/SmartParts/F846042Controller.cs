using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846042
{
public class F846042Controller : Controller
{
public new F846042WorkItem WorkItem
{
get { return base.WorkItem as F846042WorkItem; }
}
}
}
