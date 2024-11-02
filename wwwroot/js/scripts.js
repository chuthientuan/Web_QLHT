/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
// 
// Scripts
// 


const hamBurger = document.querySelector(".toggle-btn");

hamBurger.addEventListener("click", function () {
    document.querySelector("#sidebar").classList.toggle("expand");
});

var slideIndex;
// KHai bào hàm hiển thị slide
function showSlides() {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex].style.display = "block";
    //chuyển đến slide tiếp theo
    slideIndex++;
    //nếu đang ở slide cuối cùng thì chuyển về slide đầu
    if (slideIndex > slides.length - 1) {
        slideIndex = 0
    }
    //tự động chuyển đổi slide sau 5s
    setTimeout(showSlides, 3000);
}
//mặc định hiển thị slide đầu tiên 
showSlides(slideIndex = 0);

// Update the total amount dynamically
function updateTotal() {
    const rows = document.querySelectorAll('tbody tr');
    let total = 0;

    rows.forEach(row => {
        const quantity = row.querySelector('.quantity').value;
        const priceText = row.querySelector('.unit-price').innerText.replace(/[^0-9.]/g, ''); 
        const price = parseInt(priceText);
        const subtotal = quantity * price;
        row.querySelector('.subtotal').innerText = '$' + subtotal.toLocaleString('en-US'); 
        total += subtotal;
    });

    document.getElementById('subtotal').innerText = '$' + total.toLocaleString('en-US'); 
}
