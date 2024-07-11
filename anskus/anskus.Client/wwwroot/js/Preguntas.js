function addImageCorrect(idRespuesta) {
    const inpDiv = document.querySelector(`#b${idRespuesta}`);
    if (inpDiv) {
        const existingImg = inpDiv.querySelector('.btnImage');
        if (existingImg) {
            existingImg.remove();
        }
        const btn = inpDiv.querySelector('.inpCont');
        if (btn) {
            const img = document.createElement('img');
            img.className = "btnImage";
            img.src = '/img/Correct.png';
            inpDiv.insertBefore(img, btn);
        }
    } else {
        console.warn(`Element with id 'b${idRespuesta}' not found.`);
    }
}

function addImageInCorrect(idRespuesta) {
    const inpDiv = document.querySelector(`#b${idRespuesta}`);
    if (inpDiv) {
        const existingImg = inpDiv.querySelector('.btnImage');
        if (existingImg) {
            existingImg.remove();
        }
        const btn = inpDiv.querySelector('.inpCont');
        if (btn) {
            const img = document.createElement('img');
            img.className = "btnImage";
            img.src = '/img/Incorrect.png';
            inpDiv.insertBefore(img, btn);
        }
    } else {
        console.warn(`Element with id 'b${idRespuesta}' not found.`);
    }
}

function ChangeBackground() {
    const divElement = document.getElementById("footerDiv");
    if (divElement) {
        divElement.classList.remove("ContainerFooter");
        divElement.classList.add("ContainerFooterMostrar");
    } else {
        console.warn(`Element with id 'footerDiv' not found.`);
    }
}
