# Frame Rate Booster
Optimizer for Unity's Mono assemblies

This is the public repository of the Frame Rate Booster asset for Unity: https://assetstore.unity.com/packages/tools/utilities/frame-rate-booster-120660

## How it works
Unity has a lot of methods/properties that unnecessarly call the constructor on structures like Vector3 or Color. Frame Rate Booster (FRB for short) will modify your build to replace those methods/properties with optimized equivalents. To go deeper in the technical details, read this forum thread: https://forum.unity.com/threads/vector3-and-other-structs-optimization-of-operators.477338/

Those are micro-optimizations, but they can have a measurable impact when applied on code that is called very frequently.

FRB is made of mainly two parts:
* Optimizations: Contains the optimized alternatives to Unity's code.
* Optimizer: The editor code responsible for applying the optimizations.

## Room for improvement
A lot can still be done. All contributions are welcome. Examples of possible improvements:
* Implement optimizations on other strucs, such as: Bounds, Matrix4x4, Plane, Rect, RectInt, etc.
* Make FRB compatible with Android builds.

## If you feel generous ...
... let me take advantage of that :D

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=RYMFB6RRH7E3L&currency_code=EUR&source=url)
