// On page load or when changing themes, best to add inline in `head` to avoid FOUC
if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
    document.documentElement.classList.add('dark')
    } else {
    document.documentElement.classList.remove('dark')
    }

// Whenever the user explicitly chooses light mode
localStorage.theme = 'light'

// Whenever the user explicitly chooses dark mode
localStorage.theme = 'light'

// Whenever the user explicitly chooses to respect the OS preference
localStorage.removeItem('theme')



document.addEventListener("DOMContentLoaded", function () {
    var fileInput = document.getElementById('addPostUrl');
    var imagePreview = document.getElementById('addPostImage');

    if (fileInput && imagePreview) {
        fileInput.addEventListener('change', function () {
            if (this.files[0]) {
                var picture = new FileReader();
                picture.readAsDataURL(this.files[0]);
                picture.addEventListener('load', function (event) {
                    imagePreview.setAttribute('src', event.target.result);
                    imagePreview.style.display = 'block';
                });
            }
        });
    }
});



// Create Status upload image 
document.addEventListener("DOMContentLoaded", function () {
    var fileInput = document.getElementById('createStatusUrl');
    var imagePreview = document.getElementById('createStatusImage');

    if (fileInput && imagePreview) {
        fileInput.addEventListener('change', function () {
            if (this.files[0]) {
                var picture = new FileReader();
                picture.readAsDataURL(this.files[0]);
                picture.addEventListener('load', function (event) {
                    imagePreview.setAttribute('src', event.target.result);
                    imagePreview.style.display = 'block';
                });
            }
        });
    } else {
        console.warn('Elementele createStatusUrl sau createStatusImage nu exist? în DOM.');
    }
});



// create product upload image
document.addEventListener("DOMContentLoaded", function () {
    var fileInput = document.getElementById('createProductUrl');
    var imagePreview = document.getElementById('createProductImage');

    if (fileInput && imagePreview) {
        fileInput.addEventListener('change', function () {
            if (this.files[0]) {
                var picture = new FileReader();
                picture.readAsDataURL(this.files[0]);
                picture.addEventListener('load', function (event) {
                    imagePreview.setAttribute('src', event.target.result);
                    imagePreview.style.display = 'block';
                });
            }
        });
    } else {
        console.warn('Elementele createProductUrl sau createProductImage nu exist? în DOM.');
    }
});







    