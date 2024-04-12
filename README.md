# Diagnosing SSAO issue

This is something of a cold case, but I thought I'd take a crack at it. The problem is detailed here: https://gamedev.stackexchange.com/questions/85894/unwanted-darkening-at-polygon-edges-when-using-normal-maps-with-ssao

My answer to the original question "Is there anything obvious wrong with the normals?" is that there does appear to be something wrong, although it's not particularly obvious. When I finally saw it, I felt like Mac:

![Mac sees you](https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExeW5uemM3ZXlpdTUwYWYyeTVqYWUzcW41YTZsNnA2NHZhbmJnZnNodSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/9Jp1WsDkwkLtcrHBT9/giphy-downsized-large.gif)

To begin, I wanted to emulate the normal colouring shown in the screenshots. At first I thought it matched the standard Unity normal colouring, but it doesn't. One difference, as evidenced by the cube at the back right of the screen is that blue represents normals pointing towards the camera. In Unity, this is represented as negative blue and hence clipped to 0. The other difference, as evidenced by the colouring of the floor, is that the normals are in world space. If it were in camera space, the floor would effectively be tipped towards the camera and hence mix green and blue.

This project contains a modified post processing debug layer that displays the normals in world space with blue towards us for comparison purposes.

Included in this project is the scene `NormalTest`. This scene shows normal directions by reading them from the pixel under the mouse pointer and then projecting a line in the direction of the normal. This helps visualize the surfaces.

## The seat

Focussing on the seat of the chair, broadly speaking it should be a flat surface pointing up and therefore should be a flat green colour. However, the normals have been smoothed. If I make a squashed cube and smooth the normals (the `SmoothedNormals` scene), this is the colouring I get:

![flattened and smoothed cube](https://github.com/paulsinnett/SSAO/assets/3679392/5f95d0dc-a723-4ae6-a412-d7155a059e78)

A smoothed and flattened cube: it is full green in the middle, tinting red to the right and blue to the front.

![seat without normal map](https://github.com/paulsinnett/SSAO/assets/3679392/024009f0-2952-4857-a494-c79ddb5c3b52)

The seat of the original chair without normal mapping is roughly the same. It is full green in the middle, tinting red to the right and blue to the front.

![normal mapped seat](https://github.com/paulsinnett/SSAO/assets/3679392/38f0d032-e29d-46b1-a5a4-1f319242363c)

But with normal mapping, in the middle it already appears tinted towards the blue, and then it tints further from there. This doesn't appear correct.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/8abbd68a-73e4-4e94-b319-2a9c4b289da6)

For comparison, here is an asset store chair: https://assetstore.unity.com/packages/3d/props/furniture/wooden-chairs-2-variations-125757. Without normals it shows a similar pattern.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/6928749e-60e0-4baf-8abe-be620cd874c7)

With the normal map, you can see that the normal map applies changes to colours in the detail, but on average the colours remain the same over the surface.

## The legs

Focussing now on the legs, again broadly speaking, they should be cylinders coloured blue (because they should be facing you) and tinted red to the right.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/6f0070e0-08ed-4dde-89a8-7ee22acf75c6)

Again, this is roughly what we see in the chair without normal mapping.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/0e1acd7e-3258-4de3-864b-131858ca1e11)

With normal mapping applied, it appears as if the normals have been twisted to the right.

## The back

Finally, looking at the back rest, this appears to be curved and so on the left hand side, I expect the normals to be rotated towards the right.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/9052d910-cf52-411d-bdb0-b54593a30ff9)

This is what we see in the unmapped version above.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/bcc7f323-8598-4aca-9556-2c11ef755904)

However, in the normal mapped version, this tilt appears to have gone.

## Hypothesis

This suggests to me that either the normal maps have been generated incorrectly, encoded incorrectly, decoded incorrectly, or applied incorrectly.

## Excluding the shader

To eliminate the possibility of a shader error I have taken the code provided and patched it into the Unity post process SSAO effect. It appears to work correctly without demonstrating the issues noted. In this project I have included a chair and an alien model from the asset store as test files. Both of these appear to render a decent looking SSAO effect both with and without normal maps. You can use the `SSAO` scene to test this.

![image](https://github.com/paulsinnett/SSAO/assets/3679392/cac4d104-4e0d-454c-a270-f269a0370653) ![image](https://github.com/paulsinnett/SSAO/assets/3679392/93563aca-5775-41f7-8407-491623a9b409)

![image](https://github.com/paulsinnett/SSAO/assets/3679392/d72d762d-48ce-41b5-98db-5f6823700926) ![image](https://github.com/paulsinnett/SSAO/assets/3679392/2314dd64-f9db-4bb4-a50a-50bfcb01d55a)

## Conclusion

I suspect the normal maps are the cause of the issue. I would recommend checking known good normal maps and models with the engine to see if the issue was in the creation or application of the normal maps.
