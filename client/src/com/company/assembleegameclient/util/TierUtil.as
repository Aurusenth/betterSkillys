package com.company.assembleegameclient.util
{
import com.company.assembleegameclient.misc.DefaultLabelFormat;
import com.company.assembleegameclient.misc.UILabel;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.objects.ObjectProperties;
import com.company.assembleegameclient.ui.tooltip.TooltipHelper;

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
        var xml:XML = props ? ObjectLibrary.xmlLibrary_[props.type_] : null;
        var label:UILabel = null;

        if(!canShowTierTag(xml))
        {
            return null;
        }

        label = new UILabel();

        /*
            Tag widoczny na item tile / inventory.
            Każdy item, który ma mieć tag, pokazuje teraz zawsze UT.
            Kolor zależy od prawdziwego tieru.
        */
        label.text = "UT";

        DefaultLabelFormat.tierLevelLabel(label, size, getTierColor(xml));

        return label;
    }

    public static function getTooltipTierTag(props:ObjectProperties, size:int = 16) : UILabel
    {
        var xml:XML = props ? ObjectLibrary.xmlLibrary_[props.type_] : null;
        var label:UILabel = null;

        if(!canShowTierTag(xml))
        {
            return null;
        }

        label = new UILabel();

        /*
            Tekst widoczny w tooltipie.
            Tutaj pokazujemy pełną nazwę tieru.
        */
        label.text = getTierName(xml);

        DefaultLabelFormat.tierLevelLabel(label, size, getTierColor(xml));

        return label;
    }

    private static function canShowTierTag(xml:XML) : Boolean
    {
        if(xml == null)
        {
            return false;
        }

        return !isPet(xml)
                && !xml.hasOwnProperty("Consumable")
                && !xml.hasOwnProperty("NoTierTag")
                && !xml.hasOwnProperty("Treasure")
                && !xml.hasOwnProperty("PetFood");
    }

    public static function getTierName(xml:XML) : String
    {
        if(xml == null)
        {
            return "";
        }

        if(xml.hasOwnProperty("Admin"))
        {
            return "Admin";
        }

        if(xml.hasOwnProperty("Ethereal"))
        {
            return "Ethereal";
        }

        if(xml.hasOwnProperty("Ascended"))
        {
            return "Ascended";
        }

        if(xml.hasOwnProperty("Legendary"))
        {
            return "Legendary";
        }

        if(xml.hasOwnProperty("Tier"))
        {
            return "Tier " + xml.Tier;
        }

        if(xml.hasOwnProperty("@setType"))
        {
            return "Set";
        }

        return "untiered";
    }

    public static function getTierColor(xml:XML) : Number
    {
        if(xml == null)
        {
            return TooltipHelper.UNTIERED_COLOR;
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

        if(xml.hasOwnProperty("Tier"))
        {
            return 0xFFFFFF;
        }

        if(xml.hasOwnProperty("@setType"))
        {
            return TooltipHelper.SET_COLOR;
        }

        return TooltipHelper.UNTIERED_COLOR;
    }

    public static function isPet(itemDataXML:XML) : Boolean
    {
        var activateTags:XMLList = null;

        if(itemDataXML == null)
        {
            return false;
        }

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