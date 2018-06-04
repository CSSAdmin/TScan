using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI; 
namespace D846031
{
public class F846031Controller : Controller
{
public new F846031WorkItem WorkItem
{
get { return base.WorkItem as F846031WorkItem; }
}
}
}
