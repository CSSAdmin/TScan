using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846021
{
public class F846021Controller : Controller
{
public new F846021WorkItem WorkItem
{
get { return base.WorkItem as F846021WorkItem; }
}
}
}
