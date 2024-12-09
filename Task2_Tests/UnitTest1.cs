using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

public class UnitTests1
{
    [Fact]
    public void Test_IsImageFile_ValidExtensions_ReturnsTrue()
    {
        // Arrange
        string[] validExtensions = { ".bmp", ".gif", ".jpeg", ".jpg", ".png", ".tiff", ".tif" };
        Regex regexExtForImage = new Regex(@"\.(bmp|gif|tiff?|jpe?g|png)$", RegexOptions.IgnoreCase);

        // Act & Assert
        foreach (var ext in validExtensions)
        {
            Assert.True(regexExtForImage.IsMatch($"file{ext}"));
        }
    }

    [Fact]
    public void Test_IsImageFile_InvalidExtensions_ReturnsFalse()
    {
        // Arrange
        string[] invalidExtensions = { ".txt", ".doc", ".exe", ".mp4", ".zip" };
        Regex regexExtForImage = new Regex(@"\.(bmp|gif|tiff?|jpe?g|png)$", RegexOptions.IgnoreCase);

        // Act & Assert
        foreach (var ext in invalidExtensions)
        {
            Assert.False(regexExtForImage.IsMatch($"file{ext}"));
        }
    }

    [Fact]
    public void Test_CreateMirroredImage_Successful()
    {
        // Arrange
        string tempFile = Path.Combine(Path.GetTempPath(), "test_image.bmp");
        string outputFile = Path.Combine(Path.GetTempPath(), "test_image-mirrored.gif");

        // Create a temporary bitmap image
        using (Bitmap bitmap = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(Brushes.Red, 0, 0, 50, 100);
                g.FillRectangle(Brushes.Blue, 50, 0, 50, 100);
            }
            bitmap.Save(tempFile);
        }

        try
        {
            // Act
            using (Bitmap bitmap = new Bitmap(tempFile))
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                bitmap.Save(outputFile, System.Drawing.Imaging.ImageFormat.Gif);
            }

            // Assert
            Assert.True(File.Exists(outputFile));

            // Validate that the output is a GIF
            using (Bitmap resultBitmap = new Bitmap(outputFile))
            {
                Assert.Equal(100, resultBitmap.Width);
                Assert.Equal(100, resultBitmap.Height);
            }
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(outputFile)) File.Delete(outputFile);
        }
    }

    [Fact]
    public void Test_CreateMirroredImage_InvalidFile_ThrowsException()
    {
        // Arrange
        string invalidFile = Path.Combine(Path.GetTempPath(), "not_an_image.txt");
        File.WriteAllText(invalidFile, "This is not an image.");

        try
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                using (Bitmap bitmap = new Bitmap(invalidFile))
                {
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
            });
        }
        finally
        {
            // Cleanup
            if (File.Exists(invalidFile)) File.Delete(invalidFile);
        }
    }
}
