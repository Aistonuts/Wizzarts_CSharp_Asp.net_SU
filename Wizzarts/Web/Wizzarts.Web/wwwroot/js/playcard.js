const elOne = document.getElementById('previewEventTwo');
if (elOne) {
    elOne.addEventListener("click", function () {
        html2canvas(document.getElementById("backgroundTwo")).then(function (canvas) {
            var anchorTag = document.createElement("a");
            var dataURL = canvas.toDataURL("image/png", 1.0);
            dataURL = dataURL.replace('data:image/png;base64,', '');
            $("#imageData").val(dataURL);
            document.getElementById("previewEventTwo").hidden = true;
            document.getElementById("submitEventTwo").hidden = false;
            document.getElementById("startOver").hidden = false;
            document.getElementById("artByArtistV2").hidden = true;
            document.getElementById("cardTypeV2").hidden = true;
        });
    });
};

const elTwo = document.getElementById('screenshot');
if (elTwo) {
    elTwo.addEventListener("click", function () {
        html2canvas(document.getElementById("card-container")).then(function (canvas) {
            var anchorTag = document.createElement("a");
            var dataURL = canvas.toDataURL("image/png", 1.0);
            dataURL = dataURL.replace('data:image/png;base64,', '');
            $("#imageDataEventOne").val(dataURL);
            document.getElementById("submitButton").hidden = false;
            document.getElementById("submitCardImage").hidden = false;
            document.getElementById("cardData").hidden = true;
            document.getElementById("startOver").hidden = false;
            document.getElementById("cardTools").hidden = true;
        });
    });
};

$('#generate').click(function () {


    var cardName = $("#name").val();
    var AbilitiesFlavor = $("#cardtext").val();
    var cardPower = $("#power").val();
    var cardToughness = $("#toughness").val();
    var colorValues = ['{b}', '{g}', '{r}', '{t}', '{r}', '{u}', '{w}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}']


    for (let i = 0; i < colorValues.length; i++) {
        let currentValue = colorValues[i].split('');

        AbilitiesFlavor = AbilitiesFlavor.replaceAll(colorValues[i], "<img src= /Images/mana/" + currentValue[1] + ".png id=mana - icon />")
    }

    $('#cardName').html('');
    $('.centered-power').html('');
    $('.description').html('');

    $('#cardName').append(cardName);
    $('.centered-power').append(cardPower + "/" + cardToughness);
    $('.description').append(AbilitiesFlavor);


    return false;
});

function cardTypeFunc(e) {

    document.getElementById('cardtype').textContent = '';

    var currentSelection = e.target.id;
    let selectedValue = $("#" + currentSelection + " option:selected").html();

    if (selectedValue) {
        $('#cardtype').append(selectedValue)
    } else {
        return false;
    }
}

$('.insertSymbol').click(function () {
    $('#cardtext').val($('#cardtext').val() + '{' + $(this).attr('id') + '}');
    return false;
});

$('.insertManaSymbol').click(function () {

    $('#imgHeader').append("<img src= /Images/mana/" + $(this).attr('id') + ".png id=mana - icon /> ")
    return false;
});


$('.clearText').click(function () {

    $('#cardtext').val('');
    return false;
});

$('.clearTextFromCard').click(function () {

    document.getElementById("description").innerHTML = '';

});

$('#startOver').click(function () {

    location.reload();


});

function colorSelectFunc(e) {
    var currentSelection = e.target.id;

    let currentColor = currentSelection.substring(currentSelection.indexOf('_') + 1);
    let selectedValue = $("#" + currentSelection + " option:selected").val();
    console.log(selectedValue)
    if (selectedValue == "1") {

        document.querySelectorAll("#mana-icon-" + currentColor).forEach(e => e.remove());
        document.querySelectorAll("#colorless").forEach(e => e.remove());
    }
    if (currentColor == "colorless") {
        document.querySelectorAll("#colorless").forEach(e => e.remove());
        if (selectedValue != 1) {
            $('#imgHeader').append("<img src= /Images/mana/" + (selectedValue - 1) + ".png class=mana-icon  id=colorless /> ")
        } else {
            return false;
        }

    }
    else {
        var count = parseInt(selectedValue - 1, 10);
        for (let i = 0; i < count; i++) {
            $('#imgHeader').append("<img src= /Images/mana/" + currentColor + ".png class=mana-icon  id= mana-icon-" + currentColor + " /> ")

        }
    }


}


function artByArtistFunc(e) {

    document.getElementById('imageInput').innerHTML = '';

    var currentSelection = e.target.id;
    let selectedValue = $("#" + currentSelection + " option:selected").data('container');

    if (selectedValue == 0) {
        return false;

    } else {
        $('#imageInput').append("<img class= frame-art-flavor src=" + selectedValue + " />")
    }

}
function expansionSymbolFunc(e) {

    document.getElementById('symbolHeader').textContent = '';

    var currentSelection = e.target.id;
    let selectedValue = $("#" + currentSelection + " option:selected").html();

    if (selectedValue == 0) {
        return false;

    } else {
        let currentRarity = $("#rarity" + " option:selected").html();
        $('#symbolHeader').append("<img src= /images/symbols/expansions/" + selectedValue + ".png id=set - icon alt=OGW - icon />")
    }
}

//function raritySymbolFunc(e) {
//    document.getElementById('symbolHeader').textContent = '';

//    var currentSelection = e.target.id;
//    let selectedValue = $("#" + currentSelection + " option:selected").val();

//    let currentExpansion = $("#expansion" + " option:selected").val();
//    if (currentExpansion) {
//        $('#symbolHeader').append("<img src= /symbols/various/" + currentExpansion + '_' + selectedValue + ".png id=set - icon alt=OGW - icon />")
//    } else {
//        return false;
//    }
//}
function frameSelectFunc(e) {
    var currentSelection = e.target.id;
    let selectedValue = $("#" + currentSelection + " option:selected").html();

    document.getElementById("background").style["background-image"] = "url(" + '/Images/frames/' + selectedValue + ".png" + ")"

    document.getElementById("power-frame").src = "/Images/frames/" + selectedValue + "Value" + ".png";
}
