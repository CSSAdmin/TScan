using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846032
{
public class F846032Controller : Controller
{
public new F846032WorkItem WorkItem
{
get { return base.WorkItem as F846032WorkItem; }
}
}
}
