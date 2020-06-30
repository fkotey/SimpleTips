using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using UIAutomationClient;


namespace SimpleTips
{
    class UIOperator
    {
        const string EXTRUDER = "Extruder";
        const string LAYER = "Layer";
        const string ADDITIONS = "Additions";
        const string INFILL = "Infill";
        const string SUPPORT = "Support";
        const string TEMPERATURE = "Temperature";
        const string COOLING = "Cooling";
        const string GCODE = "G-Code";
        const string SCRIPTS = "Scripts";
        const string SPEEDS = "Speeds";
        const string OTHER = "Others";
        const string ADVANCED = "Advanced";

        IUIAutomation _automation;

        public UIOperator()
        {
            this._automation = new CUIAutomation();
        }

        private IUIAutomationElement pane;
        public delegate void UIScanedEventHandler(object source, EventArgs args);
        public event UIScanedEventHandler UIScanned;

        protected virtual void onUIScanned()
        {
            if (UIScanned != null)
            {
                UIScanned.Raise(this, EventArgs.Empty);
                return;
                var delegates = UIScanned.GetInvocationList();
                for (var i = 0; i < delegates.Length; i++)
                    ((UIScanedEventHandler)delegates[i]).BeginInvoke(this, EventArgs.Empty, null, null);
                
            }
                
        }
        public void executeInThread()
        {

        }
        public List<DrawItem> execute()
        {
          
            List<DrawItem> items = new List<DrawItem>();

            IUIAutomationElement boxToBorder;
            IUIAutomationElement tmpElement;
            Stopwatch watch = new Stopwatch();
           
                watch.Reset();
                watch.Start();
            IntPtr storeHwnd = IntPtr.Zero;
            foreach (Process proc in Process.GetProcesses())
            {
               if(proc.MainWindowTitle.StartsWith("Simplify3D"))
               {
                  storeHwnd = proc.MainWindowHandle;
                  
               }
            }

               // IntPtr storeHwnd = NativeMethods.FindWindow ("Qt5QWindowIcon", "Simplify3D (Licensed to ...)");
                IntPtr fffSetings = NativeMethods.FindWindow("Qt5QWindowIcon", "FFF settings");

            bool exit = false;
            if (storeHwnd == IntPtr.Zero)
                exit = true;
            if (storeHwnd != NativeMethods.GetForegroundWindow() && fffSetings != NativeMethods.GetForegroundWindow())
                exit = true;

            if (exit)
            {
                //Highlighter.BufferList(new List<DrawItem>());
                return items;
            }

            IUIAutomationElement windowApp = _automation.ElementFromHandle(storeHwnd);
                IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[1];
                conditionArray[0] = _automation.CreatePropertyCondition(UIAutomationClient.UIA_PropertyIds.UIA_NamePropertyId, "FFF Settings");
                IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
                IUIAutomationElement settingsPanel = windowApp.FindFirst(TreeScope.TreeScope_Children, conditionArray[0]);

            //can u see ?
                if (settingsPanel != null)
                {    
                    try
                    {
                        conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Auto-Configure for Material");
                        boxToBorder = settingsPanel.FindFirst(TreeScope.TreeScope_Descendants, conditionArray[0]);
                        items.Add(new DrawItem(false, boxToBorder.CurrentBoundingRectangle, Color.Orange));
                        conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Auto-Configure for Print Quality");
                        boxToBorder = settingsPanel.FindFirst(TreeScope.TreeScope_Descendants, conditionArray[0]);
                        items.Add(new DrawItem(false, boxToBorder.CurrentBoundingRectangle, Color.Blue));

                        pane = this.GetSettingsMainPane(settingsPanel);
                        var rowElement = GetRowOfTabs(settingsPanel);

                        var columnElement = GetColumnFromTabRow(rowElement, EXTRUDER);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Orange));
                        columnElement = GetColumnFromTabRow(rowElement, LAYER);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Blue));
                        columnElement = GetColumnFromTabRow(rowElement, ADDITIONS);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Blue));
                        columnElement = GetColumnFromTabRow(rowElement, INFILL);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Blue));
                        columnElement = GetColumnFromTabRow(rowElement, SUPPORT);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Blue));
                        columnElement = GetColumnFromTabRow(rowElement, TEMPERATURE);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Orange));
                        columnElement = GetColumnFromTabRow(rowElement, COOLING);
                        items.Add(new DrawItem(false, columnElement.CurrentBoundingRectangle, Color.Orange));

                        if (rowElement.CurrentName == EXTRUDER)
                        {
                            tmpElement = this.GetElementFromPane(pane, "Extrusion Multiplier");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Orange));
                        }
                        else if (rowElement.CurrentName == LAYER)
                        {
                            tmpElement = this.GetElementFromPane(pane, "Primary Layer Height");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Blue));
                            tmpElement = this.GetElementFromPane(pane, "Top Solid Layers");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Blue));
                            tmpElement = this.GetElementFromPane(pane, "Bottom Solid Layers");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Blue));
                        }
                        else if (rowElement.CurrentName == ADDITIONS)
                        {
                            tmpElement = this.GetElementFromPane(pane, "Skirt Layers");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Blue));
                        }
                        else if (rowElement.CurrentName == INFILL)
                        {
                            tmpElement = this.GetElementFromPane(pane, "Interior Fill Percentage");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Blue));
                        }
                        else if (rowElement.CurrentName == SUPPORT)
                        {
                            tmpElement = this.GetElementFromPane(pane, "Support Infill Percentage");
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Blue));
                        }
                        else if (rowElement.CurrentName == TEMPERATURE)
                        {
                            tmpElement = this.GetBoxFromPane(pane, UIA_ControlTypeIds.UIA_ListControlTypeId);
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Orange));
                        }
                        else if (rowElement.CurrentName == COOLING)
                        {
                            tmpElement = this.GetBoxFromPane(pane, UIA_ControlTypeIds.UIA_TreeControlTypeId);
                            items.Add(new DrawItem(true, tmpElement.CurrentBoundingRectangle, Color.Orange));
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("shit happens");

                    }

                    watch.Stop();
                    int timeout = (1000 / 75) - (int)watch.ElapsedMilliseconds;
                  
                //Thread.Sleep(timeout);


            }
            //Highlighter.BufferList(items);
            return items;
            // grab the context from the state
            
       

        }
     
        public IUIAutomationElement GetSettingsMainPane(IUIAutomationElement rootElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[2];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "pane");
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_PaneControlTypeId);
            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement reportElement = rootElement.FindFirst(TreeScope.TreeScope_Descendants, conditions);

            return reportElement;
        }
        public IUIAutomationElement GetRowOfTabs(IUIAutomationElement rootElement)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[2];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_TabControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "tab");
            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement reportElement = rootElement.FindFirst(TreeScope.TreeScope_Descendants, conditions);

            return reportElement;
        }
        public IUIAutomationElement GetColumnFromTabRow(IUIAutomationElement rootElement, string title)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[2];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_TabItemControlTypeId);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, title);
            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement reportElement = rootElement.FindFirst(TreeScope.TreeScope_Descendants, conditions);

            return reportElement;
        }
        public IUIAutomationElement GetElementFromPane(IUIAutomationElement rootElement, string title)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[2];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, title);
            conditionArray[1] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_TextControlTypeId);
            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement reportElement = rootElement.FindFirst(TreeScope.TreeScope_Descendants, conditions);

            return reportElement;
        }
        public IUIAutomationElement GetBoxFromPane(IUIAutomationElement rootElement, int controlType)
        {
            IUIAutomationCondition[] conditionArray = new IUIAutomationCondition[1];
            conditionArray[0] = _automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, controlType);
            IUIAutomationCondition conditions = _automation.CreateAndConditionFromArray(conditionArray);
            IUIAutomationElement reportElement = rootElement.FindFirst(TreeScope.TreeScope_Descendants, conditions);

            return reportElement;
        }
       

    }
}