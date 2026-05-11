package com.company.assembleegameclient.ui
{
   import com.company.assembleegameclient.util.FameUtil;
   import com.company.ui.SimpleText;
   import flash.display.Sprite;
   import flash.display.Shape;
   import flash.filters.DropShadowFilter;
   
   public class RankText extends Sprite
   {
       
      
      public var sprite_:Sprite = null;
      
      public var largeText_:Boolean;
      
      private var numStars_:int = -1;
      
      private var prefix_:SimpleText = null;
      
      public function RankText(numStars:int, largeText:Boolean, includePrefix:Boolean)
      {
         super();
         this.largeText_ = largeText;
         if(includePrefix)
         {
            this.prefix_ = new SimpleText(!!this.largeText_?int(18):int(16),11776947,false,0,0);
            this.prefix_.setBold(true);
            this.prefix_.text = "Rank: ";
            this.prefix_.updateMetrics();
            this.prefix_.filters = [new DropShadowFilter(0,0,0)];
            addChild(this.prefix_);
         }
         mouseEnabled = false;
         mouseChildren = false;
         this.draw(numStars);
      }
      
      public function draw(numStars:int) : void
      {
         var icon:Sprite = null;
         if(numStars == this.numStars_)
         {
            return;
         }
         this.numStars_ = numStars;
         if(this.sprite_ != null && contains(this.sprite_))
         {
            removeChild(this.sprite_);
         }
         if(this.numStars_ < 0)
         {
            return;
         }
          this.sprite_ = new Sprite();
          var text:SimpleText = new SimpleText(!!this.largeText_?int(18):int(16),11776947,false,0,0);
         text.setBold(this.largeText_);
         text.text = this.numStars_.toString();
         text.updateMetrics();
         text.filters = [new DropShadowFilter(0,0,0,1,4,4,2)];
         text.y = 1;
          // Temporarily add text to measure sizes
          this.sprite_.addChild(text);
         icon = !!this.largeText_?FameUtil.numStarsToBigImage(this.numStars_):FameUtil.numStarsToImage(this.numStars_);
         icon.x = text.width + 2;
          this.sprite_.addChild(icon);
          icon.y = int(text.height / 2 - icon.height / 2) + 1;

          // Draw semi-transparent background behind the stars and number to improve visibility
          var paddingX:int = 6;
          var paddingY:int = 6;
          var bgWidth:Number = text.width + 2 + icon.width + paddingX * 2;
          var bgHeight:Number = Math.max(text.height, icon.height) + paddingY * 2;
          var bg:Shape = new Shape();
          bg.graphics.beginFill(0x000000, 0.45);
          bg.graphics.drawRoundRect(-paddingX, -paddingY, bgWidth, bgHeight, 8, 8);
          bg.graphics.endFill();
          // Place background behind text and icon
          this.sprite_.addChildAt(bg, 0);

          addChild(this.sprite_);
         if(this.prefix_ != null)
         {
            addChild(this.prefix_);
            this.sprite_.x = this.prefix_.width;
         }
      }
   }
}
