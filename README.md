### OCR Comparer

# What do you neeed to do in order to make this thing work?
1. Get your OCR.Space, NewOcr, Google Vision Api keys
2. Put the keys in the code (look up where u need to put them for now)

# What do you need to know?
• Transkribus Api isn't implemented but does exist with the Organisational plan.

• Tesseract has been set to return a raw text instead of the hOCR. This can be changed in TesseractExecution.cs at line 35.

• All OCR implementations have the ability to get the location of the text.
