///        GeneratedTime: 2021-02-23 11:58:07
using UnityEngine;
using UnityEngine.UI;

public partial class ModelTestView : BaseUI
{

    public ModelTestView()
    {
    }
    public override BaseUI CreatGUI()
    {
        Instantiate("Views/ModelTest");
        return base.CreatGUI();
    }
}
