package com.company.assembleegameclient.util
{
import com.company.assembleegameclient.misc.DefaultLabelFormat;
import com.company.assembleegameclient.misc.UILabel;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.objects.ObjectProperties;
import com.company.assembleegameclient.ui.tooltip.TooltipHelper;
import flash.filters.DropShadowFilter;
import com.company.assembleegameclient.util.FilterUtil;

public class TierUtil
{
    private static const ADMIN_COLOR:Number = 0x8F58A9;
    private static const ETHEREAL_COLOR:Number = 0xBA1443;
    private static const ASCENDED_COLOR:Number = 0xA35282;
    private static const LEGENDARY_COLOR:Number = 0xD69A21;

    public function TierUtil()
    {
        super();
    }

    public static function getTierTag(props:ObjectProperties, size:int = 16) : UILabel
    {
        var xml:XML = ObjectLibrary.xmlLibrary_[props.type_];
        var label:UILabel = null;
        var color:Number = NaN;
        var tierTag:String = null;
        var isnotpet:* = !isPet(xml);
        var consumable:* = !xml.hasOwnProperty("Consumable");
        var noTierTag:* = !xml.hasOwnProperty("NoTierTag");
        var treasure:* = !xml.hasOwnProperty("Treasure");
        var petFood:* = !xml.hasOwnProperty("PetFood");
        var tier:Boolean = xml.hasOwnProperty("Tier");

        if(isnotpet && consumable && treasure && petFood && noTierTag)
        {
            label = new UILabel();

            if(xml.hasOwnProperty("Admin"))
            {
                color = ADMIN_COLOR;
                tierTag = "A";
            }
            else if(xml.hasOwnProperty("Ethereal"))
            {
                color = ETHEREAL_COLOR;
                tierTag = "ET";
            }
            else if(xml.hasOwnProperty("Ascended"))
            {
                color = ASCENDED_COLOR;
                tierTag = "AS";
            }
            else if(xml.hasOwnProperty("Legendary"))
            {
                color = LEGENDARY_COLOR;
                tierTag = "LG";
            }
            else if(tier)
            {
                color = 16777215;
                tierTag = "T" + xml.Tier;
            }
            else if(xml.hasOwnProperty("@setType"))
            {
                color = TooltipHelper.SET_COLOR;
                tierTag = "ST";
            }
            else
            {
                color = TooltipHelper.UNTIERED_COLOR;
                tierTag = "UT";
            }

            label.text = tierTag;
            DefaultLabelFormat.tierLevelLabel(label,size,color);
            return label;
        }

        return null;
    }

    public static function isPet(itemDataXML:XML) : Boolean
    {
        var activateTags:XMLList = null;
        activateTags = itemDataXML.Activate.(text() == "PermaPet");
        return activateTags.length() >= 1;
    }

    public static function getSpecialTierColor(xml:XML) : Number
    {
        if(xml == null)
        {
            return NaN;
        }

        if(xml.hasOwnProperty("Admin"))
        {
            return ADMIN_COLOR;
        }

        if(xml.hasOwnProperty("Ethereal"))
        {
            return ETHEREAL_COLOR;
        }

        if(xml.hasOwnProperty("Ascended"))
        {
            return ASCENDED_COLOR;
        }

        if(xml.hasOwnProperty("Legendary"))
        {
            return LEGENDARY_COLOR;
        }

        return NaN;
    }
}
}