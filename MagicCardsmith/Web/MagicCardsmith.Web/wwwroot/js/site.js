
$('#sidebarCollapse').on('click', function () {
    $('#sidebar').addClass('ae-show-sidebar');
    $('.collapse.in').toggleClass('in');
    var node = document.createElement("div");
    node.className += 'modal-backdrop fade show';
    node.setAttribute("id", "backdrop-overlay");
    document.body.appendChild(node);
});

$(document).on('click', '#backdrop-overlay, #dismiss', function () {
    $('#sidebar').removeClass('ae-show-sidebar');
    document.getElementById("backdrop-overlay").remove();
});

var globalJS =
{
    mtgSymbolConverter: function (str, newline) {
        str = str.replace(/{W}/g, '<i class="ms ms-w ms-cost ms-shadow"></i>').
            replace(/{U}/g, '<i class="ms ms-u ms-cost ms-shadow"></i>').
            replace(/{B}/g, '<i class="ms ms-b ms-cost ms-shadow"></i>').
            replace(/{R}/g, '<i class="ms ms-r ms-cost ms-shadow"></i>').
            replace(/{G}/g, '<i class="ms ms-g ms-cost ms-shadow"></i>').
            replace(/{W\/U}/g, '<i class="ms ms-wu ms-split ms-cost ms-shadow"></i>').
            replace(/{W\/B}/g, '<i class="ms ms-wb ms-split ms-cost ms-shadow"></i>').
            replace(/{U\/B}/g, '<i class="ms ms-ub ms-split ms-cost ms-shadow"></i>').
            replace(/{U\/R}/g, '<i class="ms ms-ur ms-split ms-cost ms-shadow"></i>').
            replace(/{B\/R}/g, '<i class="ms ms-br ms-split ms-cost ms-shadow"></i>').
            replace(/{B\/G}/g, '<i class="ms ms-bg ms-split ms-cost ms-shadow"></i>').
            replace(/{R\/W}/g, '<i class="ms ms-rw ms-split ms-cost ms-shadow"></i>').
            replace(/{R\/G}/g, '<i class="ms ms-rg ms-split ms-cost ms-shadow"></i>').
            replace(/{G\/W}/g, '<i class="ms ms-gw ms-split ms-cost ms-shadow"></i>').
            replace(/{G\/U}/g, '<i class="ms ms-gu ms-split ms-cost ms-shadow"></i>').
            replace(/{2\/W}/g, '<i class="ms ms-2w ms-split ms-cost ms-shadow"></i>').
            replace(/{2\/U}/g, '<i class="ms ms-2u ms-split ms-cost ms-shadow"></i>').
            replace(/{2\/B}/g, '<i class="ms ms-2b ms-split ms-cost ms-shadow"></i>').
            replace(/{2\/R}/g, '<i class="ms ms-2r ms-split ms-cost ms-shadow"></i>').
            replace(/{2\/G}/g, '<i class="ms ms-2g ms-split ms-cost ms-shadow"></i>').
            replace(/{W\/P}/g, '<i class="ms ms-wp ms-cost ms-shadow"></i>').
            replace(/{U\/P}/g, '<i class="ms ms-up ms-cost ms-shadow"></i>').
            replace(/{B\/P}/g, '<i class="ms ms-bp ms-cost ms-shadow"></i>').
            replace(/{R\/P}/g, '<i class="ms ms-rp ms-cost ms-shadow"></i>').
            replace(/{G\/P}/g, '<i class="ms ms-gp ms-cost ms-shadow"></i>').
            replace(/{C}/g, '<i class="ms ms-c ms-cost ms-shadow"></i>').
            replace(/{E}/g, '<i class="ms ms-e ms-cost ms-shadow"></i>').
            replace(/{S}/g, '<i class="ms ms-s ms-cost ms-shadow"></i>').
            replace(/{CHAOS}/g, '<i class="ms ms-chaos ms-cost ms-shadow"></i>').
            replace(/\{X}/g, '<i class="ms ms-x ms-cost ms-shadow"></i>').
            replace(/\{0}/g, '<i class="ms ms-0 ms-cost ms-shadow"></i>').
            replace(/\{1}/g, '<i class="ms ms-1 ms-cost ms-shadow"></i>').
            replace(/\{2}/g, '<i class="ms ms-2 ms-cost ms-shadow"></i>').
            replace(/\{3}/g, '<i class="ms ms-3 ms-cost ms-shadow"></i>').
            replace(/\{4}/g, '<i class="ms ms-4 ms-cost ms-shadow"></i>').
            replace(/\{5}/g, '<i class="ms ms-5 ms-cost ms-shadow"></i>').
            replace(/\{6}/g, '<i class="ms ms-6 ms-cost ms-shadow"></i>').
            replace(/\{7}/g, '<i class="ms ms-7 ms-cost ms-shadow"></i>').
            replace(/\{8}/g, '<i class="ms ms-8 ms-cost ms-shadow"></i>').
            replace(/\{9}/g, '<i class="ms ms-9 ms-cost ms-shadow"></i>').
            replace(/\{10}/g, '<i class="ms ms-10 ms-cost ms-shadow"></i>').
            replace(/\{11}/g, '<i class="ms ms-11 ms-cost ms-shadow"></i>').
            replace(/\{12}/g, '<i class="ms ms-12 ms-cost ms-shadow"></i>').
            replace(/\{13}/g, '<i class="ms ms-13 ms-cost ms-shadow"></i>').
            replace(/\{14}/g, '<i class="ms ms-14 ms-cost ms-shadow"></i>').
            replace(/\{15}/g, '<i class="ms ms-15 ms-cost ms-shadow"></i>').
            replace(/\{16}/g, '<i class="ms ms-16 ms-cost ms-shadow"></i>').
            replace(/\{17}/g, '<i class="ms ms-17 ms-cost ms-shadow"></i>').
            replace(/\{18}/g, '<i class="ms ms-18 ms-cost ms-shadow"></i>').
            replace(/\{19}/g, '<i class="ms ms-19 ms-cost ms-shadow"></i>').
            replace(/\{20}/g, '<i class="ms ms-20 ms-cost ms-shadow"></i>').
            replace(/{T}/g, '<i class="ms ms-tap ms-cost ms-shadow"></i>').
            replace(/{Q}/g, '<i class="ms ms-untap ms-cost"></i>');
        if (newline === true) {
            str = str.replace(/\n/g, '<br />');
        }
        return str;
    }
};


function search() {
    var autocompleteInput = document.getElementById("searchM");
    if (autocompleteInput) {
        $("#searchM").autocomplete({
            autoFocus: true,
            select: function (event, ui) {
                $("#searchM").val(ui.item.value);
                $(this).closest("form").submit();
            },
            source: function (request, response) {
                $.ajax({
                    url: 'https://localhost:7038/api/searchCard/',
                    type: "GET",
                    dataType: "json",
                    data: { cardName: request.term },
                    autoFocus: true,
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.combinedName, value: item.cardName, text: item.text };
                        }));
                    }
                });
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div><b>" + item.label + (item.text !== undefined && item.text !== null ? "</b><br><small class='autocomplete-desc'><i>" + globalJS.mtgSymbolConverter(item.text, false) + "</i></small>" : "") + "</div>")
                .appendTo(ul);
        };
    }
}

function searchAdv() {
    var autocompleteInput = document.getElementById("#searchCards");
    if (autocompleteInput) {
        $("#searchCards").autocomplete({
            autoFocus: true,
            select: function (event, ui) {
                $("#searchCards").val(ui.item.value);
                $(this).closest("form").submit();
            },
            source: function (request, response) {
                $.ajax({
                    url: 'https://localhost:7038/api/searchCard/',
                    type: "GET",
                    dataType: "json",
                    data: { cardName: request.term },
                    autoFocus: true,
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.combinedName, value: item.cardName, text: item.text };
                        }));
                    }
                });
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div><b>" + item.label + (item.text !== undefined && item.text !== null ? "</b><br><small class='autocomplete-desc'><i>" + globalJS.mtgSymbolConverter(item.text, false) + "</i></small>" : "") + "</div>")
                .appendTo(ul);
        };
    }
}

var createRoomBtn = document.getElementById('create-room-btn')
var createRoomModal = document.getElementById('create-room-modal')

createRoomBtn.addEventListener('click', function () {
    createRoomModal.classList.add('active')
})

function closeModal() {
    createRoomModal.classList.remove('active')
}