PixelPerfectCameraKit
=====================

PixelPerfectCameraKit aims to make working with a pixel perfect camera in Unity sane. A simple demo scene is included with a screenshot of the beautiful Chasm game (which obviously should not be used in your game!). An overview video showing what PixelPerfectCameraKit does is available [here](https://www.youtube.com/watch?v=yI8JrBNTwkc).

What PixelPerfectCameraKit does is quite simple: it renders your main Camera to an appropriately sized RenderTexture and creates a new Camera to display the RenderTexture on a quad.



Demo Scene
=====================

The included demo scene is minimal to keep things as simple as possible. It has the Chasm screenshot and a disabled dots image. The dots image is just a checkboard of black and white pixels. It is very handy for testing pixel perfect setups.



Basic Setup
=====================

- add a GameObject as a child of the Camera (or add it to the root of the scene) and put the PixelCamere2D script on it
- set the screenVerticalPixels value to your design-time resolution and modify any of the other public properties in the inspector

Setting your main camera's orthoSize to an appropriate value
=====================

 - ex: if your design-time resolution is 480x270 (16x9) and you are using 16x16 sprites
 - sprite pixels-per-unit is 16
 - divide screenVerticalPixels (270) by the sprite pixel height (16) = 16.875
 - orthoSize is half height so we use 8.4375

 - ***** NOTE: this example resolution scales up to 720p and 1080p perfectly *****
 - ex: if your design-time resolution is 320x180 (16x9) and you are using 16x16 sprites
 - sprite pixels-per-unit is 16
 - divide screenVerticalPixels (180) by the sprite pixel height (16) = 11.25
 - orthoSize is half height so we use 5.625

 - ex: if your design-time resolution is 384×216 (16x9) and you are using 16x16 sprites
 - sprite pixels-per-unit is 16
 - divide screenVerticalPixels (216) by the sprite pixel height (16) = 13.5
 - orthoSize is half height so we use 6.75

 - ex: if your design-time resolution is 960x640 (3x2) and you are using 64x64 sprites
 - sprite pixels-per-unit is 64
 - divide screenVerticalPixels (640) by the sprite pixel height (64) = 10
 - orthoSize is half height so we use 5


You can control how PixelPerfectCameraKit responds to window resizing by changing the *offAspectBehavior* field. There are three possible settings:

- **None**: does not modify the screen resolution
- **DisableCrop**: turns off cropping
- **SetNearestPerfectFitResolution**: resizes the window to the best fit that is a multiple of the design-time height


License
-----
[Attribution-NonCommercial-ShareAlike 3.0 Unported](http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode) with [simple explanation](http://creativecommons.org/licenses/by-nc-sa/3.0/deed.en_US). You are free to use the PixelPerfectCameraKit in any and all games that you make. You cannot sell the PixelPerfectCameraKit directly or as part of a larger game asset.
