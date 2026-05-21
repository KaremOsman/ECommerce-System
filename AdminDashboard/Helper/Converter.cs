using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Helper
{
    public class Converter
    {
        public  static MultipartFormDataContent ConvertDtoToMultipart(CreateOrUpdateProductDto model)
        {
            // Create a new MultipartFormDataContent object
            var content = new MultipartFormDataContent();
            // Add the properties of the model to the content
            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.Description ?? string.Empty), "Description");
            content.Add(new StringContent(model.Price.ToString()), "Price");
            content.Add(new StringContent(model.BrandId.ToString()), "BrandId");
            content.Add(new StringContent(model.TypeId.ToString()), "TypeId");
            // If the model has an image, add it to the content
            if (model.Image != null)
            {
                var fileContent = new StreamContent(model.Image.OpenReadStream());
                content.Add(fileContent, "Image", model.Image.FileName);
            }

            return content;
        }       
    }
}
