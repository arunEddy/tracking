let modal;
const API = AppConfig.apiBaseUrl + "/api/StockIssue";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('stockIssueModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Stock Issue");
        modal.show();
    });

    $("#btnSave").click(saveData);

    $("#qty").on("input", calculateBalance);
});


// ================= VALIDATION =================
function validateForm() {

    if (!$("#officeName").val().trim())
        return showError("Office required");

    if (!$("#issueDate").val())
        return showError("Date required");

    if (!$("#itemName").val().trim())
        return showError("Item required");

    if (!$("#qty").val())
        return showError("Quantity required");

    return true;
}

function showError(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
}


// ================= DATE FORMAT =================
function formatDate(dateStr) {
    if (!dateStr) return '';
    let d = new Date(dateStr);
    return d.toLocaleDateString("en-GB", {
        day: '2-digit',
        month: 'short',
        year: 'numeric'
    });
}


// ================= LOAD =================
function loadData() {

    showLoader();

    $.get(API, function (res) {

        let data = res?.data || res;

        let html = "";

        data.forEach(x => {

            html += `<tr>
                <td>${x.siId}</td>
                <td>${x.officeName}</td>
                <td>${formatDate(x.issueDate)}</td>
                <td>${x.customerName}</td>
                <td>${x.itemName}</td>
                <td>${x.quantity}</td>
                <td>${x.serialized ? 'Yes' : 'No'}</td>
                <td>${x.awb ? 'Yes' : 'No'}</td>
                <td>${x.startNo || '-'}</td>
                <td>${x.endNo || '-'}</td>
                <td>${x.balQuantity}</td>
                <td>${x.price}</td>
                <td>
                    <button class="btn btn-warning btn-sm" onclick="edit(${x.siId})">Edit</button>
                    <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.siId})">Delete</button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);

    }).always(hideLoader);
}


// ================= SAVE =================
function saveData() {

    if (!validateForm()) return;

    let id = parseInt($("#siId").val()) || 0;

    let obj = {
        siId: id,
        officeName: $("#officeName").val(),
        issueDate: $("#issueDate").val(),
        customerName: $("#customerName").val(),
        itemName: $("#itemName").val(),
        quantity: $("#qty").val(),
        price: $("#price").val(),
        serialized: $("#serialized").is(":checked"),
        awb: $("#awb").is(":checked"),
        startNo: $("#startNo").val() || null,
        endNo: $("#endNo").val() || null,
        balQuantity: $("#balQty").val()
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    showLoader();

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function () {

            Swal.fire({
                icon: "success",
                title: id > 0 ? "Updated" : "Created",
                timer: 1500,
                showConfirmButton: false
            });

            modal.hide();
            loadData();
        }
    }).always(hideLoader);
}


// ================= EDIT =================
function edit(id) {

    $.get(API + "/" + id, function (x) {

        x = x?.data || x;

        $("#siId").val(x.siId);
        $("#officeName").val(x.officeName);
        $("#issueDate").val(x.issueDate);
        $("#customerName").val(x.customerName);
        $("#itemName").val(x.itemName);
        $("#qty").val(x.quantity);
        $("#price").val(x.price);
        $("#serialized").prop("checked", x.serialized);
        $("#awb").prop("checked", x.awb);
        $("#startNo").val(x.startNo);
        $("#endNo").val(x.endNo);
        $("#balQty").val(x.balQuantity);

        modal.show();
    });
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Are you sure?",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (res.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: () => {
                    Swal.fire("Deleted!", "", "success");
                    loadData();
                }
            });
        }
    });
}


// ================= HELPERS =================
function calculateBalance() {
    let qty = parseInt($("#qty").val()) || 0;
    $("#balQty").val(qty);
}

function clearForm() {
    $("input").val('');
    $("#serialized, #awb").prop("checked", false);
}

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}