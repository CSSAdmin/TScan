using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D84603
{
public class F84603Controller : Controller
{
public new F84603WorkItem WorkItem
{
get { return base.WorkItem as F84603WorkItem; }
}
}
}
