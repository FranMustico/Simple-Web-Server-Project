const downloadButton = document.getElementById('download-button');
const textBox = document.getElementById('file-path-input');

downloadButton.addEventListener('click', () => {
    getFile()
});

textBox.addEventListener('keydown', (event) => {
  if (event.key === 'Enter') {
    event.preventDefault();
    const filePath = textBox.value;
    getFileByTextBox(filePath);
  }
});

async function getFile(){ 
  const response = await fetch('/file');

    if (response.ok){ 
      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'file.pdf';
      a.click();
      window.URL.revokeObjectURL(url);

      window.alert("File downloaded successfully!"); 
    } 
    else { 
      window.alert("Error occurred while downloading file"); 
    }
}

async function getFileByTextBox(filePath) { 
  if (!filePath || filePath.trim() === "") {
    window.alert("Please enter a valid file path.");
    return;
  }

  const encodedFilePath = encodeURI(filePath);

  try {
    const response = await fetch(`/${encodedFilePath}`); // Use the encoded path

    if (response.ok) { 
      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = filePath.split('/').pop(); // Extract the file name
      a.click();
      window.URL.revokeObjectURL(url);

      window.alert("File downloaded successfully!"); 
    } else { 
      const errorMessage = await response.text(); 
      window.alert(errorMessage && errorMessage.trim() !== "" ? errorMessage : "File not found");
    }
  } catch (error) {
    window.alert(`An unexpected error occurred: ${error.message}`);
  }
}