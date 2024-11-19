let menu = document.querySelector("#menu-bars");
let navbar = document.querySelector(".navbar");
menu.onclick = () => {
    menu.classList.toggle('fa-times');
    navbar.classList.toggle('active');
}
window.onscroll = () => {
    menu.classList.remove("fa-times");
    navbar.classList.remove("active");
};

document.querySelector('#search-icon').onclick = () =>{
    document.querySelector("#search-form").classList.toggle('active');
}
document.querySelector("#close").onclick = () => {
  document.querySelector("#search-form").classList.remove("active");
};
var icon = document.getElementById("icon");
icon.onclick = function() {
    document.body.classList.toggle("dark-theme");
    if(document.body.classList.contains("dark-theme")) {
        icon.src = "~/customer/imgs/sun.png";
    }else {
        icon.src = "~/customer/imgs/moon.png";
    }
}
let mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
  scrollFunction();
};

function scrollFunction() {
  if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
    mybutton.style.display = "block";
  } else {
    mybutton.style.display = "none";
  }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
  document.body.scrollTop = 0;
  document.documentElement.scrollTop = 0;
}


window.addEventListener("load", function () {
  setTimeout(function open(event) {
    document.querySelector(".popup").style.display = "block";
  }, 3000);
});

document.querySelector("#clos").addEventListener("click", function () {
  document.querySelector(".popup").style.display = "none";
});