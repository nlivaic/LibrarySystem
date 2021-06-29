using System.Text.Json.Serialization;

namespace LibrarySystem.Infrastructure.Scanner
{
    class MrzIdRequest
    {
        [JsonPropertyName("returnFullDocumentImage")]
        public bool ReturnFullDocumentImage { get; set; } = false;

        [JsonPropertyName("returnFaceImage")]
        public bool ReturnFaceImage { get; set; } = false;

        [JsonPropertyName("returnSignatureImage")]
        public bool ReturnSignatureImage { get; set; } = false;

        [JsonPropertyName("allowBlurFilter")]
        public bool AllowBlurFilter { get; set; } = false;

        [JsonPropertyName("allowUnparsedMrzResults")]
        public bool AllowUnparsedMrzResults { get; set; } = false;

        [JsonPropertyName("allowUnverifiedMrzResults")]
        public bool AllowUnverifiedMrzResults { get; set; } = true;

        [JsonPropertyName("validateResultCharacters")]
        public bool ValidateResultCharacters { get; set; } = true;

        [JsonPropertyName("anonymizationMode")]
        public string AnonymizationMode { get; set; } = "FULL_RESULT";

        [JsonPropertyName("anonymizeImage")]
        public bool AnonymizeImage { get; set; } = true;

        [JsonPropertyName("ageLimit")]
        public int AgeLimit { get; set; } = 0;

        [JsonPropertyName("imageSource")]
        public string ImageSource { get; set; }

        [JsonPropertyName("scanCroppedDocumentImage")]
        public bool ScanCroppedDocumentImage { get; set; } = false;

        public MrzIdRequest()
        { }

        public MrzIdRequest(string imageSource)
        {
            ImageSource = imageSource;
        }
    }
}
