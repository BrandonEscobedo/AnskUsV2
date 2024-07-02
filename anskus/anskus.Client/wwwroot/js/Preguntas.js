function addImageCorrect(idRespuesta) {
    const inpDiv = document.querySelector(`#b${idRespuesta}`);
    console.log("correcto" + idRespuesta)
    const btn = inpDiv.querySelector('.inpCont');
    const img = document.createElement('img');
    img.className = "btnImage";
    img.src = '/img/Correct.png';
    console.log(idRespuesta)
    inpDiv.insertBefore(img, btn);
        ChangeBackground();
   
}
function addImageInCorrect(idRespuesta) {
    const inpDiv = document.querySelector(`#b${idRespuesta}`);
    console.log("incorrecto" + idRespuesta)

    const btn = inpDiv.querySelector('.inpCont');
    const img = document.createElement('img');
    img.className = "btnImage";
    img.src = '/img/Incorrect.png';
    inpDiv.insertBefore(img, btn);
    ChangeBackground();
    console.log(idRespuesta+"Incorrecto")
}  
function ChangeBackground() {
    const divElement = document.getElementById("footerDiv");
    divElement.classList.remove("ContainerFooter");
    divElement.classList.add("ContainerFooterMostrar")
    console.log("papu no ")
} 