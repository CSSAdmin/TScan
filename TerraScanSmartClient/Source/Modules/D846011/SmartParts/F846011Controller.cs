using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846011
{
public class F846011Controller : Controller
{
public new F846011WorkItem WorkItem
{
get { return base.WorkItem as F846011WorkItem; }
}
}
}
