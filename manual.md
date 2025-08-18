# pixel8r Manual

## Basic Controls

The menu strip on the top left has controls to open or save a file, undo and relod.

- Open File currently supports .png, .jpg, .bmp and .gif file formats.
- Save File supports the same formats.
- Undo currently supports only going back one step.
- Reload will reset the image for the currently loaded file.

## Changing to a Predefined Palette

The first dropdown consists of palettes with a discrete set of colors. Upon selecting one, the Palette Preview pane will show squares of all the available colors in the selected palette. Once an algorithm is selected, the button to make the swap becomes available. Upon clicking this button, it will attempt to update each pixel's color to the closest available one in the palette according to the chosen algorithm.

### Algorithm Details

The color matching algorithms are based on the idea of measuring mathematical closeness of two colors in a different color space. For example, the RGB color space is probably familiar enough to not need an explanation. All of the RGB algorithms are variations of a formula that, for a "current" color C1 and a "candidate" color C2, each consisting of values R1, G1, B1 and R2, G2, B2, we want the lowest value of:

`abs(R1 - R2) + abs(G1 - G2) + abs(B1 - B2)`

Some other color spaces are explored here, and it is a similar concept. The amount of color spaces used is not currently exhaustive.

## Changing to a Programmatic Palette

Programmatic palettes change the palette based on some kind of formula instead of a discretely defined set.

#### RGB Multiples of X

All of the palettes in this category are based on factors of 255. The idea with these is if we can approximate the original color space with a smaller, simpler palette, maybe when subsequently changing to a discrete palette, there will be a better chance of matching. For each factor, 255 divided by the factor value produces the number of steps to count to 255. Add one to that to account for 0 and you get the number of values for each of R, G, B. That number cubed is the total number of available colors:

Factor | Values | Total Colors |
| --- | --- | --- |
3 | 86 | 636,056 |
5 | 52 | 140,608 |
15 | 18 | 5832 |
17 | 16 | 4096 |
51 | 6 | 216 |
85 | 4 | 64 |

#### Transpose

These algorithms are more "just for fun" since the resultant colors don't really have any significance. These swap the bytes for each of R, G and B around. So for example, in the BRG one, the red comes from the original blue value; green comes from the original red value; and blue comes from the original red value.

## Tinting

Tinting works more or less the same way as a programmatic palette, although it's more focused on accentuating a certain color. The colors that can be accentuated are:

- Primary: Red, Green, Blue
- Secondary: Cyan, Magenta, Yellow
- Black, White, Grayscale

The differences between what I've called "hard" and "soft" tinting, as well as the "scales" are explained below. There is no hard or soft tint for black and white since those colors use all bytes in the RGB space.

#### Soft Tint

Adds a small increment to only the color byte(s) that contribute to the selected tint. So for example, if you are tinting Magenta, the R and B bytes will gradually increase, with G staying the same. Can be done multiple times to intensify.

#### Hard Tint

Adds increments to the tint byte(s) just like a soft tint, but _also_ decrements the byte that doesn't contribute. So following the example from before, since the G byte will decrease, if this tint was applied repeatedly, eventually it would produce a screen of pure Magenta.

#### Scale

Gets an average value of the existing color bytes, then multiplies them by weighted values to make the desired tint color. In practice, this makes the tint much more intense than the other methods. The weight values were chosen based on experimentation. They differ because of perceptual differences in color, i.e. red tends to appear brighter than blue, so blue is weighted softer so as to avoid an overly dark appearance.

## Crop

Crop values are preselected based on common aspect ratios, including pixel perfect ratios of certain consoles. When clicking the Preview button, hovering over the picture produces a preview rectangle. Clicking on the picture when the rectangle is fully within the picture bounds will crop.

## Resize

TBD - Operation not working how I want it currently

## Pixelate

TBD - Operation not working how I want it currently

## Dither

TBD - Operation not working how I want it currently