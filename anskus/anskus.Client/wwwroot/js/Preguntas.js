function addImageCorrect(idRespuesta) {
    const inpDiv = document.querySelector(`#b${idRespuesta}`);
    const btn = inpDiv.querySelector('.inpCont');
    const img = document.createElement('img');
    img.className = "btnImage";
    img.src = '/img/Correct.png';
    inpDiv.insertBefore(img, btn);
}
function addImageInCorrect(idRespuesta) {
    const inpDiv = document.querySelector(`#b${idRespuesta}`);
    const btn = inpDiv.querySelector('.inpCont');
    const img = document.createElement('img');
    img.className = "btnImage";
    img.src = '/img/Incorrect.png';
    inpDiv.insertBefore(img, btn);
}  
function ChangeBackground() {
    const divElement = document.getElementById("footerDiv");
    divElement.classList.remove("ContainerFooter");
    divElement.classList.add("ContainerFooterMostrar")
} 