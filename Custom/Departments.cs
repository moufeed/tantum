using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace tantum.Custom
{
    public static class Departments
    {
        public const string Index = "Index";
        public const string EventManagement = "Event-Management";
        public const string DesignAndAnimation = "Design-And-Animation";
        public const string SmartSystems = "Smart-Systems";
        public const string Construction = "Construction";
        public const string SoftwareSolutions = "Software-Solutions";
        public const string Telecommunications = "Telecommunications";
        public const string GiftItems = "Gift-Items";
        public const string InteriorDesign = "Interior-Design";
         
        public static  IList<String> DepartmentsList = new ReadOnlyCollection<string>
        (new List<String> {
                Index,
                EventManagement,
                DesignAndAnimation,
                SmartSystems,
                Construction,
                SoftwareSolutions,
                Telecommunications,
                GiftItems,
                InteriorDesign
          });
    }

    
}

