# Diagnosing SSAO issue

This is something of a cold case, but I thought I'd take a crack at it. The problem is detailed here: https://gamedev.stackexchange.com/questions/85894/unwanted-darkening-at-polygon-edges-when-using-normal-maps-with-ssao

My answer to the original question "Is there anything obvious wrong with the normals?" is that there does appear to be something wrong, although it's not particularly obvious.

The first clue is that the camera is tilted down which you can see from the horizon line in the screenshots. But in images with normals coloured, the floor plain is pure green. This implies that the normal colours are world space normals with green up, red to the right, and blue towards us. If the normals were in camera space, the floor would mix green and blue based on its relative angle to the camera.

Focussing on the seat of the chair, broadly speaking it should be a flat surface pointing up and therefore should be a flat green colour. However, the normals have been smoothed. If I make a squashed cube and smooth the normals I get a similar colouring pattern as shown in the screen shots.

![flattened and smoothed cube](https://github.com/paulsinnett/SSAO/assets/3679392/5f95d0dc-a723-4ae6-a412-d7155a059e78)

A smoothed and flattened cube. It is full green in the middle, tinting red to the right and blue to the front.

![seat without normal map](https://github.com/paulsinnett/SSAO/assets/3679392/024009f0-2952-4857-a494-c79ddb5c3b52)

The seat of the chair without normal mapping is roughly the same. It is full green in the middle, tinting red to the right and blue to the front.

![normal mapped seat](https://github.com/paulsinnett/SSAO/assets/3679392/38f0d032-e29d-46b1-a5a4-1f319242363c)

With normal mapping, in the middle it already appears tinted towards the blue.

