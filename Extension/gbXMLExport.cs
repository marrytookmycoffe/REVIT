#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Analysis;
#endregion

namespace RevitExtension
{
    static class Extension
    {
        /// <summary>
        /// Export gbxml file with custom setup
        /// </summary>
        /// <param name="document"></param>
        /// <param name="energyDataSettings"></param>
        /// <returns></returns>
        public static bool Export(this Document document, EnergyDataSettings energyDataSettings)
        {
            Transaction transaction = new Transaction(document);
            transaction.Start("gbxml export");
            GBXMLExportOptions gbxmloptions = new GBXMLExportOptions();
            gbxmloptions.ExportEnergyModelType = ExportEnergyModelType.SpatialElement;

            // regenerate energy model
            EnergyAnalysisDetailModel eadm = EnergyAnalysisDetailModel.GetMainEnergyAnalysisDetailModel(document);
            if (eadm != null) document.Delete(eadm.Id);

            document.Regenerate();

            ElementId id = EnergyDataSettings.GetFromDocument(document).Id;
            Element element = document.GetElement(id);
            element = energyDataSettings;

            //add after changing default setings, when you create it he import setup 
            // new in Revit 2016:
            EnergyAnalysisDetailModelOptions energyAnalysisDetailModelOptions = new EnergyAnalysisDetailModelOptions();
            EnergyAnalysisDetailModel energyAnalysisDetailModel = EnergyAnalysisDetailModel.Create(document, energyAnalysisDetailModelOptions);

            transaction.Commit();

            string path = @document.PathName;
            int lib = path.Length - (path.LastIndexOf("\\") + 1);
            path = path.Remove(path.LastIndexOf("\\") + 1, lib);

            return document.Export(path, document.Title, gbxmloptions);
        }
    }
}
