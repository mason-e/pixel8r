# pixel8r Manual

## General Tips/Cautions

This program contains multiple options for image manipulation. Presently, most operations that change the color of the image do so on a pixel-by-pixel basis. This means some methods can be a bit slow. Additionally, operations on predefined palettes compare on each color, so these take more time the larger the palette is.

If any crop is intended, it's recommended as the first step, in order to reduce the number of pixels needed in computation. This can also be true of resizing down, but keep in mind that a resize involves a loss in pixel fidelity by nature, so the order of resize/recolor steps is not idempotent. In other words, resizing and then changing palette won't always be 100% the same as changing the palette and then resizing.

There are no failsafes against multiple operations that may not make sense together. For example, if you change the colors to a discrete palette, the image won't be "locked" to this palette. So if you then choose to tint it or saturate, the new colors won't actually be members of the palette anymore.

pixel8r does not overwrite the original loaded image, so it is safe to mess around with as much as desired.

## Example Image

The example image in this document was generated to approximate the entire RGB color space. Since 24-bit RGB is based on three values from 0-255, it would take 256^3 = 16,777,216 pixels to display every possible color at minimum - that's about two complete 4K monitors! 

Instead, it goes every 3 values, for a more manageable 86^3 - 636,056 pixels (although the actual image is a bit larger since 86 squares don't easily make for a uniform rectangle):

![](./screenshots/manual/colorspace.png)

## Basic Controls

The menu strip on the top left has controls to open or save a file, undo and relod.

- Open File currently supports .png, .jpg, .bmp and .gif file formats.
- Save File supports the same formats.
- Undo currently supports only going back one step.
- Reload will reset the image for the currently loaded file.

## Changing Palette

The first dropdown consists of palettes with a discrete set of colors. The Palette Preview pane will show squares of all the available colors in the selected palette. The selected algorithm will affect how the program attempts to update each pixel's color when the button is clicked.

Most of the palettes are sourced from this [Wiki Page](https://en.wikipedia.org/wiki/List_of_video_game_console_palettes).

### Algorithm Details

Color matching is based on the idea of a mathematical measure of the smallest "distance" between colors. More details can be found on this [Wiki Page](https://en.wikipedia.org/wiki/Color_difference). 

With the example image, we can see the differences in how different algorithms produce matches to an NES color palette:

![](./screenshots/manual/color-distance/colorspace-nes-01-rgb-euclidean.png)  
*RGB Euclidean*

![](./screenshots/manual/color-distance/colorspace-nes-02-rgb-redmean.png)  
*RGB Redmean*

![](./screenshots/manual/color-distance/colorspace-nes-03-lab-1976.png)  
*Lab CIE76*

![](./screenshots/manual/color-distance/colorspace-nes-04-lab-hybrid.png)  
*Lab Hybrid*

![](./screenshots/manual/color-distance/colorspace-nes-05-lab-1994.png)  
*Lab CIE94*

![](./screenshots/manual/color-distance/colorspace-nes-06-lch-2000.png)  
*LCh CIEDE2000*

![](./screenshots/manual/color-distance/colorspace-nes-07-cmc-a.png)  
*CMC Acceptability*

![](./screenshots/manual/color-distance/colorspace-nes-08-cmc-p.png)  
*CMC Perceptibility*

![](./screenshots/manual/color-distance/colorspace-nes-09-itp.png)  
*ITP*

![](./screenshots/manual/color-distance/colorspace-nes-10-z.png)  
*Z*

![](./screenshots/manual/color-distance/colorspace-nes-11-ok.png)  
*OK*

![](./screenshots/manual/color-distance/colorspace-nes-12-cam02.png)  
*CAM02*

![](./screenshots/manual/color-distance/colorspace-nes-13-cam16.png)  
*CAM16*


## Transpose

These algorithms are more "just for fun" since the resultant colors don't really have any significance. These swap the bytes for each of R, G and B around. So for example, in the BRG one, the red comes from the original blue value; green comes from the original red value; and blue comes from the original red value.

![](./screenshots/manual/transpose/colorspace-01-rbg.png)
*RBG*

![](./screenshots/manual/transpose/colorspace-02-grb.png) 
*GRB*

![](./screenshots/manual/transpose/colorspace-03-gbr.png)  
*GBR*

![](./screenshots/manual/transpose/colorspace-04-brg.png) 
*BRG*

![](./screenshots/manual/transpose/colorspace-05-bgr.png)  
*BGR*

## Fidelity Reduction

Many older computer system and consoles had RGB color with smaller ranges than the 0-255 we are used to on modern computers. More details can be found on this [Wiki Page](https://en.wikipedia.org/wiki/List_of_monochrome_and_RGB_color_formats#Regular_RGB_palettes)

The algorithms in this section attempt to approximate these lower fidelity colors. Of course, they may not be accurate to the actual color systems - they work by limiting the amount of values within the 24-bit RGB space that can be used.

Let's use a different color space image that shows this more prominently - this comes from Wikipedia's complete [24-Bit Colorspace](https://en.wikipedia.org/wiki/File:16777216colors.png), although it's scaled down by the program.

![](./screenshots/manual/fidelity-reduced/colorspace-01-18-bit.png)  
*18-Bit*

![](./screenshots/manual/fidelity-reduced/colorspace-02-15-bit.png)  
*15-Bit*

![](./screenshots/manual/fidelity-reduced/colorspace-03-12-bit.png)  
*12-Bit*

![](./screenshots/manual/fidelity-reduced/colorspace-04-9-bit.png)  
*9-Bit*

![](./screenshots/manual/fidelity-reduced/colorspace-05-6-bit.png)  
*6-Bit*

![](./screenshots/manual/fidelity-reduced/colorspace-06-3-bit.png)  
*3-Bit*

## Saturation

Saturation is, in plain terms, roughly a measure of the "boldness" a color. The benefit of saturation is that by making the colors "pop" a bit more, it tends to match better on a palette of simple colors. For example, on an unaltered photo it can have difficulty with skin tones or the subtle gray elements of a blue sky.

Saturation operates on a 0.0 to 1.0, or 0 to 100% scale, and the slider reflects this. A negative value would desaturate in this case.

Some things to be aware of using this:
- The saturation effect re-applies each time you use it, not just to the starting image. So if you saturate by 5%, then run it again at 15%, this will be 20% total, _not_ 15%.
- Saturation isn't idempotent in both directions necessarily. An example would be if you saturate by 30%, any pixels that were at 70% or more saturation are limited to 100%. Then if you go back by -30%, pixels that were originally _over_ 70% will be reduced to 70%.
- Another example of inconsistency is that too much desaturation can strip out the hue info, due to grays not being dictated by hue. This can be seen in the example below, but would also happen if you were to desaturate to gray and resaturate. This is because the hue range default of 0 is red.

This is the colorspace example with saturation at max:

![](./screenshots/manual/programmatic/colorspace-01-saturated.png)

## Tinting

Tinting changes the primary and secondary colors and black and white in an image by manipulating the value of one or more of the RGB channels. The dropdown reflects the color that will show more whether addition or subtraction is applied. For example, since red and cyan are complements of each other, by reducing the red channel a color appears more cyan.

Like with saturation, these add to the last version of the image each time the operation is performed. Also like saturation, adding/subtracting a value and then doing the reverse operation may not always produce the same result.

The differences between what I've called "hard" and "soft" tinting, as well as the "scales" are explained below. There is no hard or soft tint for black and white since those colors use all bytes in the RGB space.

The "scale" colors aren't affected by the soft/hard toggle _or_ the slider value.

### Soft Tint

Adds or subtracts a small increment to only the color byte that contribute to the selected tint. So for example, if you are tinting red, the R bytes will be affected, with B and G staying the same. This means the max application will still retain some of the original image.

### Hard Tint

Adds or subtracts to the tint byte just like a soft tint, but _also_ does the opposite operation at the same rate to the complement bytes. So following the example from before, the G and B bytes will be affected the same amount in the opposite direction of R. This means the max application of this would result in an image that's entirely the given color.

### Scale

Gets an average value of the existing color bytes, then multiplies them by weighted values to make the desired tint color. In practice, this makes the tint much more intense than the other methods. The weight values were chosen based on experimentation. They differ because of perceptual differences in color, i.e. red tends to appear brighter than blue, so blue is weighted softer so as to avoid an overly dark appearance.

## Crop

Crop values are preselected based on common aspect ratios, including pixel perfect ratios of certain consoles. When clicking the Preview button, hovering over the picture produces a preview rectangle. Clicking on the picture when the rectangle is fully within the picture bounds will crop.

## Resize

The resize slider automatically gets a range of values within the allowed size to expand or shrink the image to. The % value here is a bit misleading, as it's the percentage of a single dimension, not the whole area. 

## Pixelate

Makes the image look more pixelated by creating 3x3 (real pixel) sized "pixels". At present, to make even bigger pixels, you'd need to size the image down to 33%, pixelate, and then size back up to 100%, although this can produce weird results due to rounding.

## Scanlines

Produces a scanline effect using subtle brightness changes every other line. Reapplying this will affect it again, but is not recommended as it tends to just make the whole image look brighter. Like pixelation, the scanlines can be made bigger by sizing down before applying and then sizing back up.