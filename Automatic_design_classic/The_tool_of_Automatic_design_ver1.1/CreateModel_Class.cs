using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.sw3dprinter;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swdimxpert;
using SolidWorks.Interop.swdocumentmgr;
using SolidWorks.Interop.swmotionstudy;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.SWRoutingLib;

namespace The_tool_of_Automatic_design_ver1._1
{
    class CreateModel_Class
    {
        SldWorks swApp = new SldWorks();

        ModelDoc2 swModel;
        Feature swFeature;
        Configuration config;
        CustomPropertyManager cusPropMgr;

        public void CreateHC(ref double dout, ref double di, ref double h) //중공축 생성
        {
            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(0, 0, 0, dout/2); //외경(반지름) m단위
            swModel.CreateCircleByRadius2(0, 0, 0, di/2); //내경(반지름)

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Hollow_Clynder_Sketch";

            status = swModel.Extension.SelectByID2("Hollow_Clynder_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Hollow_Clynder_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Hollow_clynder / Boru", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
        }

        public void CreateC(ref double d,ref double h) // 중실축 생성
        {
            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(0, 0, 0, d / 2); //외경(반지름) m단위

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Clynder_Sketch";

            status = swModel.Extension.SelectByID2("Clynder_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Clynder_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "clynder", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
        }

        public void CreateB(ref double x, ref double y, ref double h) //각주 생성
        {
            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-x / 2, -y / 2, 0, x / 2, y / 2, 0, true);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Block_Sketch";

            status = swModel.Extension.SelectByID2("Block_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Block_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Block / Boru", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
        }
    }
}
