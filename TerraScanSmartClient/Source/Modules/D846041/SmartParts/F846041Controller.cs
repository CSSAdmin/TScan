using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846041
{
public class F846041Controller : Controller
{
public new F846041WorkItem WorkItem
{
get { return base.WorkItem as F846041WorkItem; }
}
}
}
