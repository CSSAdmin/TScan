using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846022
{
public class F846022Controller : Controller
{
public new F846022WorkItem WorkItem
{
get { return base.WorkItem as F846022WorkItem; }
}
}
}
