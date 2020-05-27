using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public static class Utility
{
   public static void StartBackgroundTask(IEnumerator update, Action end = null)
   {
       EditorApplication.CallbackFunction closureCallback = null;
 
       closureCallback = () =>
       {
           try
           {
               if (update.MoveNext() == false)
               {
                   if (end != null) end();
                   EditorApplication.update -= closureCallback;
               }
           }
           catch (Exception ex)
           {
               if (end != null) end();
               Debug.LogException(ex);
               EditorApplication.update -= closureCallback;
           }
       };
 
       EditorApplication.update += closureCallback;
   }
}