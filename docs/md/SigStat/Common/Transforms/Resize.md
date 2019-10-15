# [Resize](./Resize.md)

Namespace: [SigStat]() > [Common](./../README.md) > [Transforms](./README.md)

Assembly: SigStat.Common.dll

Implements [ILoggerObject](./../ILoggerObject.md), [IProgress](./../Helpers/IProgress.md), [IPipelineIO](./../Pipeline/IPipelineIO.md), [ITransformation](./../ITransformation.md)

## Summary
Resizes the image to a specified width and height

## Constructors

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>Resize (  )</sub>| <sub></sub>| <br>


## Properties

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>Height</sub>| <sub>The new height. Leave it as null, if you do not want to explicitly specify a given height</sub>| <br>
| <sub>InputImage</sub>| <sub>Input [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) describing the image of the signature</sub>| <br>
| <sub>OutputImage</sub>| <sub>Output [FeatureDescriptor](https://github.com/sigstat/sigstat/blob/develop/docs/md/SigStat/Common/FeatureDescriptor.md) describing the resized image of the signature</sub>| <br>
| <sub>ResizeFunction</sub>| <sub>Set a resize function if you want to dynamically calculate the new width and height of the image</sub>| <br>
| <sub>Width</sub>| <sub>The new width. Leave it as null, if you do not want to explicitly specify a given width</sub>| <br>


## Methods

| Name<img width=475> | Summary<img width=475> | 
| --- | --- | 
| <sub>[Transform](./Methods/Resize-100663716.md) ( [`Signature`](./../Signature.md) )</sub>| <sub></sub>| <br>


