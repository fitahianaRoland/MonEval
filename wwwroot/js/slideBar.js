const sidebar = document.querySelector('.sidebar');
const liElements = document.querySelectorAll('.background_sidebar .sidebar ul > li');

for (let i = 0; i < liElements.length; i++) {
    liElements[i].addEventListener('click', (e) => {
        const arrow = liElements[i];
        const subItem = arrow.querySelector('.sub_item');
        if (subItem) {
            subItem.classList.toggle('active');
        }   
        arrow.querySelector('.item').classList.toggle('active');
        //arrow.querySelector('.chevronDown').classList.toggle('active');   
    });
}

let bugerMenu = document.querySelector('.burger_menu');
const background_sidebar = document.querySelector('.background_sidebar');
bugerMenu.addEventListener('click',()=>{
    // const isHidden = sidebar.style.left === '-300px'
    const isHidden = sidebar.getAttribute("isHidden");
    // background_sidebar.classList.toggle('active');
    if (isHidden==="true") {
        sidebar.style.left = '0';
        sidebar.setAttribute("isHidden","false")
    } else {
        sidebar.style.left = '-300px';
        sidebar.setAttribute("isHidden","true")
    }
})

// background_sidebar.addEventListener('click', () => {
//     sidebar.style.left = '-300px'; // Réinitialiser la position de la barre latérale
//     background_sidebar.classList.remove('active'); // Masquer background_sidebar
// });