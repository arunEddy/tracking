var modal;
let items = [];
let deletedItems = [];

const API = AppConfig.apiBaseUrl + "/api/BookingSelf";
const ITEM_API = AppConfig.apiBaseUrl + "/api/BookingSelfItemDetails";

$(document).ready(function () {
    $("#ActualWeight, #VolWt").on("input", function () {
        let actual = parseFloat($("#ActualWeight").val()) || 0;
        let vol = parseFloat($("#VolWt").val()) || 0;

        let charge = Math.max(actual, vol);
        $("#ChargeWt").val(charge);
    });
    let el = document.getElementById('bookingModal');
    if (el) modal = new bootstrap.Modal(el);

    loadData();

    $("#btnAdd").click(openModal);
    $("#btnSave").click(saveData);
    $("#btnAddItem").click(addItem);

    $(document).on("click", ".btn-edit", function () {
        edit($(this).data("id"));
    });

    $(document).on("click", ".btn-delete", function () {
        deleteRec($(this).data("id"));
    });

    $(document).on("click", ".btn-item-delete", function () {
        removeItem($(this).data("index"));
    });

    $(document).on("click", ".btn-item-edit", function () {
        editItem($(this).data("index"));
    });
});

function calculateChargeWt() {
    let actual = parseFloat($("#ActualWeight").val()) || 0;
    let vol = parseFloat($("#VolWt").val()) || 0;

    $("#ChargeWt").val(Math.max(actual, vol));
}

// event binding
$("#ActualWeight, #VolWt").on("input", calculateChargeWt);
// ================= LOAD =================
function loadData() {

    $.get(API, function (res) {

        let data = res?.data || res;
        let html = "";

        data.forEach((x, i) => {

            html += `<tr>
                <td>${i + 1}</td>
                <td>${x.awb || ''}</td>
                <td>${x.customerName || ''}</td>
                <td>${x.origin || ''}</td>
                <td class="text-end">
                    <button class="btn btn-warning btn-sm btn-edit" data-id="${x.bseid}">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-danger btn-sm btn-delete" data-id="${x.bseid}">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);
    });
}


// ================= OPEN MODAL =================
function openModal() {
    clearForm();
    $("#modalTitle").text("Add Booking");
    modal.show();
}


// ================= VALIDATION =================
function validate() {

    if (!$("#CustomerName").val()) return Swal.fire("Customer Name required"), false;
    if (!$("#AWB").val()) return Swal.fire("AWB required"), false;
    if (!$("#Origin").val()) return Swal.fire("Origin required"), false;
    if (!$("#Destination").val()) return Swal.fire("Destination required"), false;
    if (!$("#Pcs").val()) return Swal.fire("Pcs required"), false;
    if (!$("#ChargeWt").val()) return Swal.fire("Charge Weight required"), false;

    return true;
}


// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#bseid").val()) || 0;
    let isUpdate = id > 0;

    let obj = {

        bseid: id,
        BookingOffice: $("#BookingOffice").val(),
        CustomerName: $("#CustomerName").val(),
        DocketType: $("#DocketType").val(),

        AWB: $("#AWB").val(),
        BookDate: $("#BookDate").val(),

        Origin: $("#Origin").val(),
        Destination: $("#Destination").val(),
        Mode: $("#Mode").val(),

        Pcs: parseInt($("#Pcs").val()) || 0,
        ActualWeight: parseFloat($("#ActualWeight").val()) || 0,
        Volumetric: parseFloat($("#Volumetric").val()) || 0,
        VolWt: parseFloat($("#VolWt").val()) || 0,
        ChargeWt: parseFloat($("#ChargeWt").val()) || 0,

        ProductName: $("#ProductName").val(),
        TopayAmount: parseFloat($("#TopayAmount").val()) || 0,
        ProductType: $("#ProductType").val(),

        ConsignorName: $("#ConsignorName").val(),
        PickupCity: $("#PickupCity").val(),
        PickupPincode: $("#PickupPincode").val(),
        Address1: $("#Address1").val(),
        Address2: $("#Address2").val(),

        ConsigneeName: $("#ConsigneeName").val(),
        ConsigneeAddress1: $("#ConsigneeAddress1").val(),
        ConsigneeAddress2: $("#ConsigneeAddress2").val(),
        ConsigneePhone: $("#ConsigneePhone").val(),
        Mobile: $("#Mobile").val(),
        ConsigneePincode: $("#ConsigneePincode").val(),
        City: $("#City").val(),

        MasterReferenceNo: parseInt($("#MasterReferenceNo").val()) || 0,
        Remarks: $("#Remarks").val(),

        IsActive: "Y"
    };

    let type = isUpdate ? "PUT" : "POST";
    let url = isUpdate ? API + "/" + id : API;

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function (res) {

            let mainId = res?.data?.bseid || res?.bseid || id;

            saveItems(mainId, isUpdate);
        },
        error: function (err) {
            console.log(err.responseText);
            Swal.fire("Error", "Save failed", "error");
        }
    });
}


// ================= SAVE ITEMS =================
//Arun 
function saveItems(mainId, isUpdate) {

    let requests = [];

    // DELETE ITEMS
    deletedItems.forEach(x => {
        requests.push($.ajax({
            url: ITEM_API + "/" + x.btdId,
            type: "DELETE"
        }));
    });

    // INSERT / UPDATE
    items.forEach(x => {

        let obj = {
            
            btdId: x.btdId || 0,
            PartnerRefNo: x.PartnerRefNo,
            eWayBillNumber: x.eWayBillNumber,
            EWBValidDate: x.EWBValidDate,

            InvoiceID: x.InvoiceID,
            InvoiceDate: x.InvoiceDate,
            Description: x.Description,
            InvoiceAmount: parseFloat(x.InvoiceAmount) || 0,
            CODAmount: parseFloat(x.CODAmount) || 0,

            bseid: mainId
        };

        if (obj.btdId > 0) {
            requests.push($.ajax({
                url: ITEM_API + "/" + obj.btdId,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(obj)
            }));
        } else {
            requests.push($.ajax({
                url: ITEM_API,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(obj)
            }));
        }
    });

    $.when.apply($, requests).then(() => {

        Swal.fire("Success", isUpdate ? "Updated" : "Saved", "success");

        modal.hide();
        loadData();

    }).fail(err => {
        console.log(err.responseText);
        Swal.fire("Error", "Item save failed", "error");
    });
}

function edit(id) {

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;

        // ===== BASIC =====
        $("#bseid").val(x.bseid);
        $("#BookingOffice").val(x.bookingOffice);
        $("#CustomerName").val(x.customerName);
        $("#DocketType").val(x.docketType);
        $("#AWB").val(x.awb);
        $("#BookDate").val(formatDateInput(x.bookDate));

        // ===== ROUTE =====
        $("#Origin").val(x.origin);
        $("#Destination").val(x.destination);
        $("#Mode").val(x.mode);

        // ===== WEIGHT =====
        $("#Pcs").val(x.pcs);
        $("#ActualWeight").val(x.actualWeight);
        $("#Volumetric").val(x.volumetric);
        $("#VolWt").val(x.volWt);
        $("#ChargeWt").val(x.chargeWt);

        // ===== PRODUCT =====
        $("#ProductName").val(x.productName);
        $("#TopayAmount").val(x.topayAmount);
        $("#ProductType").val(x.productType);

        // ===== CONSIGNOR =====
        $("#ConsignorName").val(x.consignorName);
        $("#PickupCity").val(x.pickupCity);
        $("#PickupPincode").val(x.pickupPincode);
        $("#Address1").val(x.address1);
        $("#Address2").val(x.address2);

        // ===== CONSIGNEE =====
        $("#ConsigneeName").val(x.consigneeName);
        $("#ConsigneeAddress1").val(x.consigneeAddress1);
        $("#ConsigneeAddress2").val(x.consigneeAddress2);
        $("#ConsigneePhone").val(x.consigneePhone);
        $("#Mobile").val(x.mobile);
        $("#ConsigneePincode").val(x.consigneePincode);
        $("#City").val(x.city);

        // ===== EXTRA =====
        $("#MasterReferenceNo").val(x.masterReferenceNo);
        $("#Remarks").val(x.remarks);

        // ===== LOAD CHILD =====
        loadItems(id);

        modal.show();

        calculateChargeWt();
    });
}

function loadItems(id) {

    $.ajax({
        url: ITEM_API + "/" + id,   // correct route
        type: "GET",
        success: function (res) {

            console.log("ITEM API RESPONSE:", res);

            // 🔥 IMPORTANT FIX
            let data = res?.data || [];

            // अगर single object आया तो array बनाओ
            if (!Array.isArray(data)) {
                data = [data];
            }

            items = data.map(x => ({
                btdId: x.btdId,
                PartnerRefNo: x.partnerRefNo,
                eWayBillNumber: x.eWayBillNumber,
                EWBValidDate: formatDateInput(x.ewbValidDate),

                InvoiceID: x.invoiceID,
                InvoiceDate: formatDateInput(x.invoiceDate),
                Description: x.description,
                InvoiceAmount: x.invoiceAmount,
                CODAmount: x.codAmount
            }));

            deletedItems = [];

            renderItems();
        },
        error: function (err) {
            console.log("LOAD ITEM ERROR:", err.responseText);
            Swal.fire("Error", "Item load failed", "error");
        }
    });
}
function formatDateInput(date) {
    if (!date) return '';

    let d = new Date(date);

    let month = ('0' + (d.getMonth() + 1)).slice(-2);
    let day = ('0' + d.getDate()).slice(-2);

    return d.getFullYear() + '-' + month + '-' + day;
}

// ================= ITEM =================
function addItem() {

    let index = $("#InvoiceID").data("index");

    let item = {
        btdId: 0,
        PartnerRefNo: $("#PartnerRefNo").val(),
        eWayBillNumber: $("#eWayBillNumber").val(),
        EWBValidDate: $("#EWBValidDate").val(),
        InvoiceID: $("#InvoiceID").val(),
        InvoiceDate: $("#InvoiceDate").val(),
        Description: $("#Description").val(),
        InvoiceAmount: $("#InvoiceAmount").val(),
        CODAmount: $("#CODAmountItem").val()
    };

    if (index !== undefined) {
        item.btdId = items[index].btdId;
        items[index] = item;
        $("#InvoiceID").removeData("index");
    } else {
        items.push(item);
    }

    renderItems();
    clearItem();
}
function formatDate(d) {
    if (!d) return '';
    let dt = new Date(d);
    return dt.toLocaleDateString('en-GB'); // dd-mm-yyyy
}
function editItem(i) {

    let x = items[i];

    $("#PartnerRefNo").val(x.PartnerRefNo);
    $("#eWayBillNumber").val(x.eWayBillNumber);
    $("#EWBValidDate").val(x.EWBValidDate);
    $("#InvoiceID").val(x.InvoiceID).data("index", i);
    $("#InvoiceDate").val(x.InvoiceDate);
    $("#Description").val(x.Description);
    $("#InvoiceAmount").val(x.InvoiceAmount);
    $("#CODAmountItem").val(x.CODAmount);
}

function removeItem(i) {

    let item = items[i];

    if (item.btdId > 0) deletedItems.push(item);

    items.splice(i, 1);
    renderItems();
}

function renderItems() {

    let html = "";

    items.forEach((x, i) => {

        html += `<tr>
            <td>${i + 1}</td>
            <td>${x.PartnerRefNo || ''}</td>
            <td>${x.eWayBillNumber || ''}</td>
            <td>${x.EWBValidDate || ''}</td>
            <td>${x.InvoiceID || ''}</td>
           <td>${formatDate(x.InvoiceDate)}</td>
            <td>${x.Description || ''}</td>
            <td>${x.InvoiceAmount || 0}</td>
            <td>${x.CODAmount || 0}</td>
            <td>
                <button class="btn btn-warning btn-sm btn-item-edit" data-index="${i}">✏️</button>
                <button class="btn btn-danger btn-sm btn-item-delete" data-index="${i}">🗑️</button>
            </td>
        </tr>`;
    });

    $("#itemTable").html(html);
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete?",
        text: "This will delete main + all item details",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (!res.isConfirmed) return;

        // 🔥 STEP 1: get all items for this booking
        $.ajax({
            url: ITEM_API + "/by-booking/" + id,   // ⚠️ your custom API
            type: "GET",
            success: function (response) {

                let data = response?.data || [];

                // ensure array
                if (!Array.isArray(data)) data = [data];

                let deleteRequests = [];

                // 🔥 STEP 2: delete all child records
                data.forEach(x => {
                    if (x.btdId) {
                        deleteRequests.push(
                            $.ajax({
                                url: ITEM_API + "/" + x.btdId,
                                type: "DELETE"
                            })
                        );
                    }
                });

                // 🔥 STEP 3: after child delete → delete main
                $.when.apply($, deleteRequests).always(function () {

                    $.ajax({
                        url: API + "/" + id,
                        type: "DELETE",
                        success: () => {
                            Swal.fire("Deleted", "Main + Items deleted", "success");
                            loadData();
                        },
                        error: (err) => {
                            console.log(err.responseText);
                            Swal.fire("Error", "Main delete failed", "error");
                        }
                    });

                });
            },
            error: function (err) {
                console.log("FETCH ITEM ERROR:", err.responseText);
                Swal.fire("Error", "Failed to fetch item details", "error");
            }
        });
    });
}


// ================= CLEAR =================
function clearForm() {

    $("input").val('');
    $("select").val('');

    items = [];
    deletedItems = [];

    renderItems();
}

function clearItem() {
    $("#PartnerRefNo,#eWayBillNumber,#EWBValidDate,#InvoiceID,#InvoiceDate,#Description,#InvoiceAmount,#CODAmountItem").val('');
}