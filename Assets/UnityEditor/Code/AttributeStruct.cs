/*==============================================================================
* 文件名:     NewBehaviourScript 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 3:22:54 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/
using UnityEngine;
using System.Collections;

public class AttributeStruct : PropertyAttribute
{

    public int max;
    public int min;

    public AttributeStruct(int a, int b)
    {
        min = a;
        max = b;
    }
}


