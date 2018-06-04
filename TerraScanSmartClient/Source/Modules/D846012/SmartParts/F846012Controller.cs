using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846012
{
public class F846012Controller : Controller
{
public new F846012WorkItem WorkItem
{
get { return base.WorkItem as F846012WorkItem; }
}
}
}
