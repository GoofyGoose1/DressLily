let currentIndex = 0;

if ("scrollRestoration" in history) {
    history.scrollRestoration = "manual";
}

const scrollKey = "lilyScroll:" + window.location.pathname;
const formScrollKey = "lilyScroll:afterFormSubmit";
let scrollSaveQueued = false;

function getScrollFromUrl() {
    const params = new URLSearchParams(window.location.search);
    const scrollY = params.get("scrollY");
    return scrollY ? Number(scrollY) : null;
}

function removeScrollFromUrl() {
    const url = new URL(window.location.href);
    if (!url.searchParams.has("scrollY")) {
        return;
    }

    url.searchParams.delete("scrollY");
    window.history.replaceState({}, "", url.pathname + url.search + url.hash);
}

function saveScrollPosition() {
    sessionStorage.setItem(scrollKey, String(window.scrollY));
}

function saveFormScrollPosition() {
    const scrollY = String(window.scrollY);
    sessionStorage.setItem(scrollKey, scrollY);
    sessionStorage.setItem(formScrollKey, scrollY);
}

function queueScrollSave() {
    if (scrollSaveQueued) {
        return;
    }

    scrollSaveQueued = true;
    requestAnimationFrame(function () {
        saveScrollPosition();
        scrollSaveQueued = false;
    });
}

function restoreScrollPosition() {
    const urlScroll = getScrollFromUrl();
    let savedScroll = urlScroll !== null ? String(urlScroll) : sessionStorage.getItem(scrollKey);
    const usedFormFallback = urlScroll === null && !savedScroll && sessionStorage.getItem(formScrollKey);

    if (!savedScroll) {
        savedScroll = sessionStorage.getItem(formScrollKey);
    }

    if (!savedScroll) {
        return;
    }

    const scrollY = Number(savedScroll);
    if (Number.isNaN(scrollY)) {
        return;
    }

    function applySavedScroll() {
        window.scrollTo(0, scrollY);
    }

    applySavedScroll();
    requestAnimationFrame(applySavedScroll);
    window.addEventListener("load", applySavedScroll, { once: true });
    setTimeout(applySavedScroll, 80);
    setTimeout(applySavedScroll, 180);
    setTimeout(applySavedScroll, 360);

    if (urlScroll !== null) {
        sessionStorage.setItem(scrollKey, String(scrollY));
        removeScrollFromUrl();
    }

    if (usedFormFallback) {
        sessionStorage.removeItem(formScrollKey);
    }
}

function setFormScrollInput(form) {
    let scrollInput = form.querySelector('input[name="scrollY"]');
    if (!scrollInput) {
        scrollInput = document.createElement("input");
        scrollInput.type = "hidden";
        scrollInput.name = "scrollY";
        form.appendChild(scrollInput);
    }

    scrollInput.value = String(window.scrollY);
}

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
    const counterElement = document.getElementById("productCounter");
    const moodElement = document.getElementById("previewMood");
    const productCard = document.getElementById("productCard");

    if (!nameElement || !imageElement || !priceElement || !descriptionElement || !selectedIdElement || !lilyElement) {
        return;
    }

    if (productCard) {
        productCard.classList.remove("is-changing");
        void productCard.offsetWidth;
        productCard.classList.add("is-changing");
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

    priceElement.innerText = "$" + product.price;
    descriptionElement.innerText = product.description || "A tiny wardrobe piece designed for Lily's pixel fashion studio.";

    if (counterElement) {
        const current = String(currentIndex + 1).padStart(2, "0");
        const total = String(products.length).padStart(2, "0");
        counterElement.innerText = current + " / " + total;
    }

    if (moodElement) {
        moodElement.innerText = product.name ? "Trying on " + product.name : "Ready for a new look";
    }

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
    restoreScrollPosition();

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

    const productCard = document.getElementById("productCard");
    if (productCard) {
        productCard.addEventListener("animationend", function () {
            productCard.classList.remove("is-changing");
        });
    }

    const pageRoot = document.getElementById("pageRoot");
    if (pageRoot) {
        requestAnimationFrame(function () {
            pageRoot.classList.add("page-transition-active");
        });
    }

    document.body.addEventListener("click", function (event) {
        const link = event.target.closest("a");
        if (!link || link.target === "_blank" || link.hasAttribute("download")) {
            return;
        }

        const hrefAttr = link.getAttribute("href") || link.href;
        if (!hrefAttr || hrefAttr.startsWith("#") || hrefAttr.startsWith("javascript:")) {
            return;
        }

        const absoluteHref = link.href;
        if (absoluteHref.indexOf(window.location.origin) !== 0) {
            return;
        }

        event.preventDefault();
        saveScrollPosition();
        if (pageRoot) {
            pageRoot.classList.add("page-transition-exit");
        }

        setTimeout(function () {
            window.location.href = hrefAttr;
        }, 260);
    }, true);

    document.querySelectorAll("form").forEach(function (form) {
        form.addEventListener("submit", function () {
            setFormScrollInput(form);
            saveFormScrollPosition();
        });
    });

    document.addEventListener("scroll", queueScrollSave, { passive: true });
    window.addEventListener("beforeunload", saveScrollPosition);
});
