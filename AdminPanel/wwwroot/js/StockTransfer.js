let modal;
const API = AppConfig.apiBaseUrl + "/api/StockTransfer";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('stockTransferModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Stock Transfer");
        modal.show();
    });

    $("#btnSave").click(saveData);

    $("#quantity").on("input", function () {
        $("#balQty").val($(this).val());
    });
});


// LOAD
function loadData() {

    showLoader();

    $.get(API, function (res) {

        let data = res?.data || res;
        let html = "";

        data.forEach((x, i) => {

            html += `<tr>
                <td>${i + 1}</td>
                <td>${formatDate(x.transferDate)}</td>
                <td>${x.fromOffice}</td>
                <td>${x.toOffice}</td>
                <td>${x.itemName}</td>
                <td>${x.quantity}</td>
                 <td>${x.startNo}</td>
                  <td>${x.endNo}</td>
                <td>${x.balQuantity}</td>

                <td class="text-end">
                    <button class="btn btn-sm btn-outline-warning me-1" onclick="edit(${x.stid})">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-sm btn-outline-danger" onclick="deleteRec(${x.stid})">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);

    }).always(hideLoader);
}


// SAVE
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#id").val()) || 0;


    let user = sessionStorage.getItem("username") || "Admin";

    let obj = {
        stid: id,
        transferDate: $("#transferDate").val(),
        fromOffice: $("#fromOffice").val(),
        toOffice: $("#toOffice").val(),
        itemName: $("#itemName").val(),
        quantity: parseInt($("#quantity").val()),
        startNo: $("#startNo").val(),
        endNo: $("#endNo").val(),
        balQuantity: $("#balQty").val(),
        isActive: $("#isActive").val(),
        createdBy: id === 0 ? user : null,
        modifiedBy: id > 0 ? user : null
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    Swal.fire({
        title: id ? "Update record?" : "Create record?",
        icon: "question",
        showCancelButton: true
    }).then(r => {

        if (!r.isConfirmed) return;

        showLoader();

        $.ajax({
            url: url,
            type: type,
            contentType: "application/json",
            data: JSON.stringify(obj),

            success: () => {
                Swal.fire({
                    icon: "success",
                    title: id ? "Updated Successfully" : "Created Successfully",
                    timer: 1500,
                    showConfirmButton: false
                });
                modal.hide();
                loadData();
            },
            error: function (err) {
                console.error(err);
                Swal.fire("Error", "Something went wrong", "error");
            },

           

            complete: hideLoader
        });
    });
}


// EDIT
function edit(id) {

    $.get(API + "/" + id, function (x) {

        x = x?.data || x;

        $("#id").val(x.stid);
        $("#transferDate").val(x.transferDate.split('T')[0]);
        $("#fromOffice").val(x.fromOffice);
        $("#toOffice").val(x.toOffice);
        $("#itemName").val(x.itemName);
        $("#quantity").val(x.quantity);
        $("#startNo").val(x.startNo);
        $("#endNo").val(x.endNo);
        $("#balQty").val(x.balQuantity);
        $("#isActive").val(mapIsActive(x.isActive));
        modal.show();
    });
}
function mapIsActive(val) {
    val = (val || "").toString().trim().toLowerCase();
    return (val === "yes" || val === "true" || val === "1" || val === "y") ? "Yes" : "No";
}

// DELETE
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        icon: "warning",
        showCancelButton: true
    }).then(r => {

        if (!r.isConfirmed) return;

        $.ajax({
            url: API + "/" + id,
            type: "DELETE",
            success: () => {
                Swal.fire("Deleted!", "", "success");
                loadData();
            }
        });
    });
}


// VALIDATION
function validate() {

    if (!$("#transferDate").val()) return Swal.fire("Validation", "Date required", "warning"), false;
    if (!$("#fromOffice").val()) return Swal.fire("Validation", "From Office required", "warning"), false;
    if (!$("#toOffice").val()) return Swal.fire("Validation", "To Office required", "warning"), false;
    if (!$("#itemName").val()) return Swal.fire("Validation", "Item required", "warning"), false;
    if (!$("#quantity").val()) return Swal.fire("Validation", "Quantity required", "warning"), false;

    return true;
}


// HELPERS
function clearForm() {
    $("#stockTransferModal input").val('');
}

function formatDate(d) {
    return d ? new Date(d).toLocaleDateString("en-GB") : '';
}

function showLoader() {
    $("body").append('<div id="loader" class="spinner-border text-danger" style="position:fixed;top:50%;left:50%;z-index:9999;"></div>');
}

function hideLoader() {
    $("#loader").remove();
}