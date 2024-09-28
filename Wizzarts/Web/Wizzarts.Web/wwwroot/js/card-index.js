function cardSearch() {
    document.getElementById("cards").innerHTML = '<div class="w-100 text-center"><h2>Searching...</h3><br/><img src="/images/loading.svg" alt="Loading" height="156" width="143" /></div>';
    var form = $('#searchform').serializeArray();
    var JSONform = objectifyForm(form);
    $('#searchSortName').html('<b><i class="fas fa-sort-amount-up"></i> Name</b>');
    $('#searchSortNumber').html('<b>Number</b>');
    $('#ae-price-sort').html('<b>Price</b>');
    $.ajax({
        url: "/Card/Search",
        type: "POST",
        dataType: "json",
        data: JSONform,
        success: function (data) {
            if (data.success === true) {
                var output = '';
                Object.keys(data.cards).forEach(function (item) {
                    output += '<div class="inner-container"><div class="item-hidden-text">' + data.cards[item].name + '</div>';
                    output += '<a href="/Card/' + data.cards[item].set + "/" + formatUrlName(data.cards[item].name) + '/' + data.cards[item].number + '" data-name="' + data.cards[item].name + '" data-number="' + data.cards[item].number + '" data-img="' + data.cards[item].img + '	?quality=60" data-tcgprice="' + data.cards[item].usd + '" data-mcmprice="' + data.cards[item].eur + '" class="item ae-card-link cardLink">';
                    output += ' <img class="lazy" src="/images/cardDeadLink3.jpg" data-src="' + data.cards[item].img + '?quality=60" alt="' + data.cards[item].name + '" />';
                    output += '</a> <div class="item-footer">';
                    if (data.cards[item].usd > 0) {
                        output += '<span class="item-price ae-price-usd">$' + data.cards[item].usd + '</span>';
                    }
                    if (data.cards[item].eur > 0) {
                        output += '<span class="item-price ae-price-eur">&euro;' + data.cards[item].eur + '</span>';
                    }
                    if (data.cards[item].tix > 0) {
                        output += '<span class="item-price ae-price-tix">' + data.cards[item].tix + '</span>';
                    }
                    output += '</div></div>';
                });
                document.getElementById("cardSearchToggles").classList.remove("d-none");
                document.getElementById("cards").innerHTML = output;
                $(function () {
                    $('.lazy').lazy();
                });
                var url = '?name=' + JSONform.name + '&format=' + JSONform.format + '&color=' + JSONform.color + '&conly=' + JSONform.conly + '&cop=' + JSONform.cop + '&artist=' + JSONform.artist + '&rarity=' + JSONform.rarity + '&text=' + JSONform.text + '&set=' + JSONform.set + '&stat=' + JSONform.stat + '&op=' + JSONform.op + '&uniq=' + JSONform.uniq + '&val=' + JSONform.val + '&type=' + JSONform.type;
                window.history.pushState(JSONform, 'MTG Card Search', url);
            }
            else {
                document.getElementById("cardSearchToggles").classList.add("d-none");
                document.getElementById("cards").innerHTML = '<h3>No cards found with the current parameters. Search might be too wide</h3>';
            }
        },
        error: function () {
            document.getElementById("cards").innerHTML = '<h2>An Error occurred</h2>';
        }
    });
}

function loadInputs() {
    var urlParams = new URLSearchParams(window.location.search);
    var conly = '';
    var cop = '';
    var uniq = '';
    for (entry of urlParams.entries()) {
        switch (entry[0]) {
            case 'name':
                $('#searchCards').val(entry[1]);
                break;
            case 'format':
                $('#adv-formatSelector').val(entry[1]);
                break;
            case 'color':
                $('#colorString').val(entry[1]);
                var colors = entry[1].split('|');
                $('#colorSelector').val(colors);
                break;
            case 'conly':
                conly = entry[1];
                break;
            case 'cop':
                cop = entry[1];
                break;
            case 'artist':
                $('#adv-ArtistText').val(entry[1]);
                break;
            case 'rarity':
                $('#adv-raritySelector').val(entry[1]);
                break;
            case 'text':
                $('#adv-CardText').val(entry[1]);
                break;
            case 'set':
                $('#adv-setSelector').val(entry[1]);
                break;
            case 'stat':
                $('#adv-CardAttr').val(entry[1]);
                break;
            case 'op':
                $('#adv-CardOperator').val(entry[1]);
                break;
            case 'uniq':
                uniq = entry[1];
                break;
            case 'val':
                $('#adv-CardVal').val(entry[1]);
                break;
            case 'type':
                $('#adv-CardType').val(entry[1]);
                break;
            default:
                break;
        }
        if (conly === 'false' && cop === 'and') {
            $('#color-only').val('false');
            $('#color-and').val('and');
            $('#colorOperator').val('false-and');
        } else if (conly === 'true' && cop === 'and') {
            $('#color-only').val('true');
            $('#color-and').val('and');
            $('#colorOperator').val('true-and');
        } else if (conly === 'true' && cop === 'or') {
            $('#color-only').val('true');
            $('#color-and').val('or');
            $('#colorOperator').val('true-or');
        } else if (conly === 'false' && cop === 'or') {
            $('#color-only').val('false');
            $('#color-and').val('or');
            $('#colorOperator').val('false-or');
        }
        $('#colorOperator').selectpicker('refresh');
        if (uniq === 'true') {
            $('#adv-unique').val('true');
            $('#adv-unique-btn').addClass('active').attr('aria-pressed', 'true');
        } else {
            $('#adv-unique').val('false');
            $('#adv-unique-btn').removeClass('active').attr('aria-pressed', 'false');
        }
        $('#adv-raritySelector').selectpicker('refresh');
        $('#adv-formatSelector').selectpicker('refresh');
        $('#adv-CardAttr').selectpicker('refresh');
        $('#adv-CardOperator').selectpicker('refresh');
        $('#adv-setSelector').selectpicker('refresh');
        $('#colorSelector').selectpicker('refresh');
    }
}

$(document).ready(function () {
    loadInputs();
    $('#colorOperator').on('change', function () {
        switch ($(this).val()) {
            case 'false-and':
                $('#color-only').val('false');
                $('#color-and').val('and');
                break;
            case 'true-and':
                $('#color-only').val('true');
                $('#color-and').val('and');
                break;
            case 'true-or':
                $('#color-only').val('true');
                $('#color-and').val('or');
                break;
            case 'false-or':
                $('#color-only').val('false');
                $('#color-and').val('or');
                break;
            default:
                break;
        }
    });

    $('#colorSelector').on('change', function () {
        string = $(this).val().join('|');
        $('#colorString').val(string);
    });

    $('.ae-btn-type').click(function () {
        $('#adv-CardType').val($(this).attr('data-type'));
    });

    $('.ae-btn-type').click(function () {
        $('#adv-CardTypeId').val($(this).attr('data-id'));
    });

    $('.ae-btn-keyword').click(function () {
        $('#adv-CardText').val($(this).attr('data-keyword'));
    });

    $('#adv-unique-btn').click(function () {
        if ($('#adv-unique').val() === 'true') {
            $('#adv-unique').val('false');
            popupMessage('<b>Unique card filter off</b>', 3000, true);
        } else {
            $('#adv-unique').val('true');
            popupMessage('<b>Unique card filter on</b>', 3000, false);
        }
    });


    $('#searchCards').on('keyup', function (e) {
        if (e.keyCode === 13) {
            cardSearch();
        }
    });

    $('#adv-CardType').on('keyup', function (e) {
        if (e.keyCode === 13) {
            cardSearch();
        }
    });
    $('#adv-CardText').on('keyup', function (e) {
        if (e.keyCode === 13) {
            cardSearch();
        }
    });
    $('#adv-ArtistText').on('keyup', function (e) {
        if (e.keyCode === 13) {
            cardSearch();
        }
    });

    $('#resetFilter').click(function () {
        $('#searchCards').val('');
        $('#adv-CardType').val('');
        $('#adv-CardText').val('');
        $('#adv-ArtistText').val('');
        $('#adv-setSelector').val('');
        $('#adv-setSelector').selectpicker('refresh');
        $('#adv-raritySelector').val('');
        $('#adv-raritySelector').selectpicker('refresh');
        $('#adv-formatSelector').val('');
        $('#adv-formatSelector').selectpicker('refresh');
        $('#adv-CardAttr').val('cmc');
        $('#adv-CardAttr').selectpicker('refresh');
        $('#adv-CardOperator').val('=');
        $('#adv-CardOperator').selectpicker('refresh');
        $('#adv-CardVal').val('');
        $('#colorString').val('');
        $('#colorSelector').val('');
        $('#colorSelector').selectpicker('refresh');
        $('#colorOperator').val('false-and');
        $('#colorOperator').selectpicker('refresh');
        $('#searchSortName').html('<b>Name</b>').attr('data-method', 'asc');
        $('#searchSortNumber').html('<b>Number</b>').attr('data-method', 'asc');
        $('#ae-price-sort').html('<b>Price</b>').attr('data-method', 'asc');
        $('#ae-price-toggle').html('<b>$ / €</b>').attr('data-toggle', 'usd-eur').attr('data-nexttoggle', 'eur');
        $('#adv-unique').val('false');
        $('#adv-unique-btn').removeClass('active').attr('aria-pressed', 'false');
    });

    $('#reset-searchCards').click(function () {
        $('#searchCards').val('');
    });

    $('#reset-adv-CardType').click(function () {
        $('#adv-CardType').val('');
    });

    $('#reset-adv-CardText').click(function () {
        $('#adv-CardText').val('');
    });

    $('#reset-adv-setSelector').click(function () {
        $('#adv-setSelector').val('');
        $('#adv-setSelector').selectpicker('refresh');
    });

    $('#reset-adv-formatSelector').click(function () {
        $('#adv-formatSelector').val('');
        $('#adv-formatSelector').selectpicker('refresh');
    });

    $('#reset-adv-raritySelector').click(function () {
        $('#adv-raritySelector').val('');
        $('#adv-raritySelector').selectpicker('refresh');
    });

    $('#reset-adv-ArtistText').click(function () {
        $('#adv-ArtistText').val('');
    });

    $('#reset-adv-CardVal').click(function () {
        $('#adv-CardAttr').val('cmc');
        $('#adv-CardAttr').selectpicker('refresh');
        $('#adv-CardOperator').val('=');
        $('#adv-CardOperator').selectpicker('refresh');
        $('#adv-CardVal').val('');
        $('#colorString').val('');
    });

    $('#reset-adv-colorString').click(function () {
        $('#colorString').val('');
        $('#colorSelector').val('');
        $('#colorSelector').selectpicker('refresh');
        $('#colorOperator').val('false-and');
        $('#colorOperator').selectpicker('refresh');
    });
    var hasSetBeenSelected = $('#selectedSetCode');
    if (hasSetBeenSelected && hasSetBeenSelected.val()) {
        console.log(hasSetBeenSelected);
        $('#adv-setSelector').val(hasSetBeenSelected.val());
        $('#adv-setSelector').selectpicker('refresh');
    }
});
