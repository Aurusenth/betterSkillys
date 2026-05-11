package kabam.rotmg.ui.view
{
import com.company.assembleegameclient.constants.ScreenTypes;
import com.company.assembleegameclient.screens.AccountScreen;
import com.company.assembleegameclient.screens.TitleMenuOption;
import com.company.assembleegameclient.ui.SoundIcon;
import com.company.ui.SimpleText;

import flash.display.Bitmap;
import flash.display.BitmapData;
import flash.display.Graphics;
import flash.display.Sprite;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.filters.DropShadowFilter;
import flash.geom.Point;

import kabam.rotmg.ui.model.EnvironmentData;
import kabam.rotmg.ui.view.components.DarkenFactory;
import org.osflash.signals.Signal;

public class TitleView extends Sprite
{
   // Nowy obraz (szeroki)
   [Embed(source="New HomeScreen Wide.png")]
   private static var NewHomeScreen:Class;

   private static const COPYRIGHT:String = "© betterSkillys :)";


   public var playClicked:Signal;
   public var serversClicked:Signal;
   public var creditsClicked:Signal;
   public var accountClicked:Signal;
   public var legendsClicked:Signal;
   public var editorClicked:Signal;

    private var container:Sprite;
    private var graphic:Sprite;
    private var background:Bitmap;
    private var flamesLayer:Bitmap;
    private var flamesSensitivityX:Number = 0.06;
    private var flamesSensitivityY:Number = 0.04;
    private var flamesEase:Number = 0.14;

   private var playButton:TitleMenuOption;
   private var serversButton:TitleMenuOption;
   private var creditsButton:TitleMenuOption;
   private var accountButton:TitleMenuOption;
   private var legendsButton:TitleMenuOption;
   private var editorButton:TitleMenuOption;

   private var versionText:SimpleText;
   private var copyrightText:SimpleText;
   private var darkenFactory:DarkenFactory;
   private var data:EnvironmentData;
    public static var anchor:Point = new Point(-40, -40);
    public static var anchor2:Point = new Point(0, -20);

   public function TitleView()
   {
      this.darkenFactory = new DarkenFactory();
      super();
      this.initScreen();
        // Inicjuj parallax dla warstwy płomieni
        this.initParallax();
      //this.graphic = this.makeScreenGraphic();
      //addChild(this.graphic);
      // Dodaj ekran konta nad tłem, aby pokazać informacje o użytkowniku i gwiazdach
      addChild(new AccountScreen());
      //this.makeChildren();
      addChild(new SoundIcon());
   }

   private function initScreen():void {
      // Dodanie nowego obrazu jako tła
      var asset:Object = new NewHomeScreen();
      if (asset is BitmapData) {
         this.background = new Bitmap(asset as BitmapData);
      } else if (asset is Bitmap) {
         this.background = asset as Bitmap;
      } else {
         try {
            this.background = new Bitmap(asset as BitmapData);
         } catch (e:Error) {
            this.background = new Bitmap();
         }
      }
      // Wstaw tło na sam dół stosu wyświetlania
      addChildAt(this.background, 0);
   }

   private function initParallax():void {
      // Stwórz warstwę płomieni i dodaj nad tłem
      try {
         this.flamesLayer = new TitleView_FlamesLayer();
      } catch (e:Error) {
         this.flamesLayer = null;
      }
      if (this.flamesLayer != null) {
         this.flamesLayer.x = 0;
         this.flamesLayer.y = 20;
         addChild(this.flamesLayer);
         this.flamesLayer.addEventListener(Event.ENTER_FRAME, onFlamesParallax);
      }
   }

   private function onFlamesParallax(e:Event):void {
      if (this.flamesLayer == null || !stage) return;
      // Compute target based on mouse position relative to stage center for natural parallax
      var centerX:Number = stage.stageWidth * 0.5;
      var centerY:Number = stage.stageHeight * 0.5;
      var dx:Number = (mouseX - centerX) * this.flamesSensitivityX;
      var dy:Number = (mouseY - centerY) * this.flamesSensitivityY;
      var targetX:Number = anchor2.x + dx;
      var targetY:Number = anchor2.y + dy;
      // Smoothly interpolate towards target
      this.flamesLayer.x += (targetX - this.flamesLayer.x) * this.flamesEase;
      this.flamesLayer.y += (targetY - this.flamesLayer.y) * this.flamesEase;
   }

    // Parallax layers removed to prefer single embedded background image

   private function makeScreenGraphic():Sprite
   {
      var box:Sprite = new Sprite();
      var b:Graphics = box.graphics;
      b.clear();
      b.beginFill(0, 0.5);
      b.drawRect(0, 0, 1, 75);
      b.endFill();
      addChild(box);
      return box;
   }

   private function makeChildren() : void
   {
      this.container = new Sprite();
      this.playButton = new TitleMenuOption(ScreenTypes.PLAY,36,true);
      this.playButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.playClicked = this.playButton.clicked;
      this.container.addChild(this.playButton);
      this.serversButton = new TitleMenuOption(ScreenTypes.SERVERS,22,false);
      this.serversButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.serversClicked = this.serversButton.clicked;
      this.container.addChild(this.serversButton);
      this.creditsButton = new TitleMenuOption(ScreenTypes.CREDITS,22,false);
      this.creditsClicked = this.creditsButton.clicked;
      //this.container.addChild(this.creditsButton);
      this.accountButton = new TitleMenuOption(ScreenTypes.ACCOUNT,22,false);
      this.accountButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.accountClicked = this.accountButton.clicked;
      this.container.addChild(this.accountButton);
      this.legendsButton = new TitleMenuOption(ScreenTypes.LEGENDS,22,false);
      this.legendsButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.legendsClicked = this.legendsButton.clicked;
      this.container.addChild(this.legendsButton);
      this.editorButton = new TitleMenuOption(ScreenTypes.EDITOR,22,false);
      this.editorButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.editorClicked = this.editorButton.clicked;
      this.versionText = new SimpleText(12,0xaaaaaa,false,0,0);
      this.versionText.filters = [new DropShadowFilter(0,0,0)];
      this.container.addChild(this.versionText);
      this.copyrightText = new SimpleText(12,0xaaaaaa,false,0,0);
      this.copyrightText.text = COPYRIGHT;
      this.copyrightText.updateMetrics();
      this.copyrightText.filters = [new DropShadowFilter(0,0,0)];
      this.container.addChild(this.copyrightText);
   }

   public function addListeners():void
   {
      this.playButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.serversButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.accountButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.legendsButton.addEventListener(MouseEvent.CLICK, removeListener);
      this.editorButton.addEventListener(MouseEvent.CLICK, removeListener);
   }

   public function removeListener(e:Event):void
   {
      if (stage)
         stage.removeEventListener("resize", positionButtons);
      this.playButton.removeEventListener(MouseEvent.CLICK, removeListener);
      this.serversButton.removeEventListener(MouseEvent.CLICK, removeListener);
      this.accountButton.removeEventListener(MouseEvent.CLICK, removeListener);
      this.legendsButton.removeEventListener(MouseEvent.CLICK, removeListener);
      this.editorButton.removeEventListener(MouseEvent.CLICK, removeListener);
   }

   public function initialize(data:EnvironmentData) : void
   {
       this.data = data;
       // Upewnij się, że elementy UI istnieją zanim spróbujemy je zaktualizować
       if (this.container == null || this.versionText == null || this.copyrightText == null) {
          this.makeChildren();
       }

       // Upewnij się, że mamy graphic zanim ustawimy pozycje
       if (this.graphic == null) {
          this.graphic = this.makeScreenGraphic();
       }

       this.updateVersionText();
       this.positionButtons();
       this.addChildren();
       this.addListeners();
      if (stage)
         stage.addEventListener("resize", positionButtons);
   }

   private function updateVersionText() : void
   {
      this.versionText.htmlText = this.data.buildLabel;
      this.versionText.updateMetrics();
   }

   private function addChildren() : void
   {
      addChild(this.container);
      this.container.addChild(this.editorButton);
   }

   public function positionButtons(e:Event = null) : void
   {
      if (stage)
      {
         if (e != null)
            AccountScreen.reSize(e);
          this.graphic.width = stage.stageWidth;
          this.graphic.y = stage.stageHeight - 75;
           // Jeśli mamy własne tło, dopasuj jego rozmiar
           if (this.background != null) {
              this.background.width = stage.stageWidth;
              this.background.height = stage.stageHeight;
           }
            if (this.flamesLayer != null) {
               this.flamesLayer.scaleX = stage.stageWidth / 800;
               this.flamesLayer.scaleY = stage.stageHeight / 600;
            }

         this.playButton.x = stage.stageWidth / 2 - this.playButton.width / 2;
         this.playButton.y = stage.stageHeight - 75;
         this.serversButton.x = stage.stageWidth / 2 - this.serversButton.width / 2 - 94;
         this.serversButton.y =  stage.stageHeight - 65;
         this.accountButton.x = stage.stageWidth / 2 - this.accountButton.width / 2 + 96;
         this.accountButton.y = stage.stageHeight - 65;
         this.legendsButton.x = this.accountButton.x + 96;
         this.legendsButton.y = stage.stageHeight - 65;
         this.editorButton.x = this.serversButton.x - 96;
         this.editorButton.y = stage.stageHeight - 65;
         this.versionText.y = stage.stageHeight - this.versionText.height;
         this.copyrightText.x = stage.stageWidth - this.copyrightText.width;
         this.copyrightText.y = stage.stageHeight - this.copyrightText.height;
      }
   }
}

}
