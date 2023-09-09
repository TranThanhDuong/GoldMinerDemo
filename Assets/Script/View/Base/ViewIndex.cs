using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView=0,
    HomeView,
    ShopView,
}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices = {
     ViewIndex.EmptyView,
     ViewIndex.HomeView,
     ViewIndex.ShopView,
     };
}
public class ViewParam
{

}
public class HomeViewParam: ViewParam
{

}