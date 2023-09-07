 protected override void Execute(CodeActivityContext context)
 {
     try
     {
         using (Image imageFile = Image.FromFile(tifFilePath.Get(context)))
         {
             FrameDimension frameDimensions = new FrameDimension(
                 imageFile.FrameDimensionsList[0]);

             // Gets the number of pages from the tiff image (if multipage) 
             int frameNum = imageFile.GetFrameCount(frameDimensions);
             string[] jpegPaths = new string[frameNum];

             for (int frame = 0; frame < frameNum; frame++)
             {
                 // Selects one frame at a time and save as jpeg. 
                 imageFile.SelectActiveFrame(frameDimensions, frame);
                 using (Bitmap bmp = new Bitmap(imageFile))
                 {
                     jpegPaths[frame] = String.Format("{0}\\{1}{2}.jpg",
                         outputFolder.Get(context),
                         Path.GetFileNameWithoutExtension(tifFilePath.Get(context)),
                         frame);
                     bmp.Save(jpegPaths[frame], ImageFormat.Jpeg);
                 }
             }
         }
     }
     catch (Exception)
     {

         throw;
     }
 }
