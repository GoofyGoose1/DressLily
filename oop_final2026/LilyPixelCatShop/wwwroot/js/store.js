let currentIndex = 0;

function showProduct() {
    if (typeof products === "undefined" || products.length === 0) {
        return;
    }

    const product = products[currentIndex];

    const nameElement = document.getElementById("productName");
    const imageElement = document.getElementById("productImage");
    const priceElement = document.getElementById("productPrice");
    const descriptionElement = document.getElementById("productDescription");
    const selectedIdElement = document.getElementById("selectedId");
    const lilyElement = document.getElementById("lilyImage");

    if (!nameElement || !imageElement || !priceElement || !descriptionElement || !selectedIdElement || !lilyElement) {
        return;
    }

    nameElement.innerText = product.name;

    if (product.image) {
        imageElement.style.display = "inline-block";
        imageElement.src = product.image;
        imageElement.alt = product.name;
    } else {
        imageElement.style.display = "none";
        imageElement.src = "";
        imageElement.alt = "";
    }

    priceElement.innerText = "Price: $" + product.price;
    descriptionElement.innerText = product.description;

    selectedIdElement.value = product.id;

    if (product.catImage && product.catImage !== "/images/") {
        lilyElement.src = product.catImage;
    } else {
        lilyElement.src = "/images/LilyRegular.PNG";
    }
}

function nextProduct() {
    if (typeof products === "undefined" || products.length === 0) {
        return;
    }

    currentIndex++;

    if (currentIndex >= products.length) {
        currentIndex = 0;
    }

    showProduct();
}

function previousProduct() {
    if (typeof products === "undefined" || products.length === 0) {
        return;
    }

    currentIndex--;

    if (currentIndex < 0) {
        currentIndex = products.length - 1;
    }

    showProduct();
}

document.addEventListener("DOMContentLoaded", function () {
    showProduct();

    if (localStorage.getItem("catBought") === "true") {
        const lilyElement = document.getElementById("lilyImage");

        if (lilyElement) {
            lilyElement.src = "/images/LilyHappy.PNG";

            setTimeout(function () {
                showProduct();
                localStorage.removeItem("catBought");
            }, 1000);
        }
    }
});