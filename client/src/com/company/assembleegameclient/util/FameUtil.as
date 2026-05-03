package com.company.assembleegameclient.util
{
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.rotmg.graphics.StarGraphic;
import com.company.util.AssetLibrary;
import flash.display.BitmapData;
import flash.display.Sprite;
import flash.display.DisplayObject;
import flash.filters.DropShadowFilter;
import flash.geom.ColorTransform;

public class FameUtil
{
   public static const STARS:Vector.<int> = new <int>[20,150,400,800,2000];

   private static const lightBlueCT:ColorTransform = new ColorTransform(138 / 255,152 / 255,222 / 255);
   private static const darkBlueCT:ColorTransform = new ColorTransform(49 / 255,77 / 255,219 / 255);
   private static const redCT:ColorTransform = new ColorTransform(193 / 255,39 / 255,45 / 255);
   private static const orangeCT:ColorTransform = new ColorTransform(247 / 255,147 / 255,30 / 255);
   private static const yellowCT:ColorTransform = new ColorTransform(255 / 255,255 / 255,0 / 255);
   private static const adminCT:ColorTransform = new ColorTransform(0 / 255, 255 / 255, 80 / 255);

   public static const COLORS:Vector.<ColorTransform> = new <ColorTransform>[lightBlueCT,darkBlueCT,redCT,orangeCT,yellowCT];

   public function FameUtil()
   {
      super();
   }

   public static function maxStars() : int
   {
      return ObjectLibrary.playerChars_.length * STARS.length;
   }

   public static function numStars(fame:int) : int
   {
      var num:int = 0;
      while(num < STARS.length && fame >= STARS[num])
      {
         num++;
      }
      return num;
   }

   public static function nextStarFame(bestFame:int, currFame:int) : int
   {
      var curr:int = Math.max(bestFame,currFame);
      for(var i:int = 0; i < STARS.length; i++)
      {
         if(STARS[i] > curr)
         {
            return STARS[i];
         }
      }
      return -1;
   }

   // Pomocnicza funkcja, żeby nie powtarzać kodu kolorów
   public static function numStarsToColorTransform(numStars:int) : ColorTransform
   {
      if (numStars >= 100) return adminCT;
      if (numStars < ObjectLibrary.playerChars_.length) return lightBlueCT;
      if (numStars < ObjectLibrary.playerChars_.length * 2) return darkBlueCT;
      if (numStars < ObjectLibrary.playerChars_.length * 3) return redCT;
      if (numStars < ObjectLibrary.playerChars_.length * 4) return orangeCT;
      return yellowCT;
   }

   public static function numStarsToImage(numStars:int) : Sprite
   {
      var star:Sprite = new StarGraphic();
      star.transform.colorTransform = numStarsToColorTransform(numStars);
      return star;
   }

   // TEJ METODY BRAKOWAŁO - to ona naprawi błąd w RankText.as
   public static function numStarsToBigImage(numStars:int) : Sprite
   {
      var star:Sprite = numStarsToImage(numStars);
      star.scaleX = 1.8;
      star.scaleY = 1.8;
      return star;
   }

   public static function numStarsToIcon(numStars:int) : Sprite
   {
      var star:Sprite = numStarsToImage(numStars);
      var sprite:Sprite = new Sprite();
      sprite.graphics.beginFill(0,0.4);
      var w:int = star.width / 2 + 2;
      var h:int = star.height / 2 + 2;
      sprite.graphics.drawCircle(w,h,w);
      star.x = 2;
      star.y = 1;
      sprite.addChild(star);
      sprite.filters = [new DropShadowFilter(0,0,0,0.5,6,6,1)];
      return sprite;
   }

   public static function getFameIcon() : BitmapData
   {
      var fameBD:BitmapData = AssetLibrary.getImageFromSet("lofiObj3",224);
      return TextureRedrawer.redraw(fameBD,40,true,0);
   }
}
}