let modal;

const API = AppConfig.apiBaseUrl + "/api/CashBooking";
const ITEM_API = AppConfig.apiBaseUrl + "/api/CashBookingItemDetails";

let items = [];
let deletedItems = [];

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('bookingModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Booking");
        modal.show();
    });

    $("#btnSave").click(saveData);
    $("#btnAddItem").click(addItem);

    $("#btnGetRate").click(getRate);
    $("#btnSendMail").click(sendMail);
    $("#btnPrint").click(printInvoice);

    $("#receivedAmount").on("input", function () {

        let received = parseFloat($(this).val()) || 0;

        let cState = ($("#cState").val() || "").toString().trim().toLowerCase();
        let cnState = ($("#cnState").val() || "").toString().trim().toLowerCase();

        let tariff = 0, cgst = 0, sgst = 0, igst = 0;

        if (received <= 0) {
            $("#tariffAmount,#cgst,#sgst,#igst,#total").val('');
            return;
        }

        // 🔥 ALWAYS reverse calculate
        tariff = received / 1.18;

        // 🔥 IMPORTANT FIX: अगर state empty है → default CGST+SGST
        if (!cState || !cnState || cState === cnState) {

            cgst = tariff * 0.09;
            sgst = tariff * 0.09;
            igst = 0;

        } else {

            igst = tariff * 0.18;
            cgst = 0;
            sgst = 0;
        }

        $("#tariffAmount").val(tariff.toFixed(2));
        $("#cgst").val(cgst.toFixed(2));
        $("#sgst").val(sgst.toFixed(2));
        $("#igst").val(igst.toFixed(2));
        $("#total").val(received.toFixed(2));
    });
    $(document).on("click", ".btn-delete", function () {
        removeItem($(this).data("index"));
    });

    $(document).on("click", ".btn-edit", function () {
        edit($(this).data("id"));
    });

    $(document).on("click", ".btn-delete-main", function () {
        deleteRec($(this).data("id"));
    });

    $("#cState, #cnState").on("change", function () {

        let received = $("#receivedAmount").val();

        if (received) {
            $("#receivedAmount").trigger("input");
        } else {
            calculateGST(); // fallback
        }
    });
});


// ================= LOAD =================
function loadData() {

    $.get(API, function (res) {

        let data = res?.data || res;
        let html = "";

        data.forEach((x, i) => {
            html += `<tr>
                <td>${i + 1}</td>
                <td>${x.awb || ''}</td>
                <td>${x.consignorName || ''}</td>
                <td>${x.consigneeName || ''}</td>
                <td>${x.total || ''}</td>
                <td class="text-end">
                    <button class="btn btn-warning btn-sm btn-edit" data-id="${x.cbid}">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-danger btn-sm btn-delete-main" data-id="${x.cbid}">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);
    });
}


// ================= DELETE =================
function deleteRec(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "This record will be deleted!",
        icon: "warning",
        showCancelButton: true
    }).then((res) => {
        if (res.isConfirmed) {
            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: () => {
                    Swal.fire("Deleted!", "Record deleted", "success");
                    loadData();
                }
            });
        }
    });
}


// ================= VALIDATION =================
function validate() {

    if (!$("#awb").val()) return Swal.fire("AWB required"), false;
    if (!$("#origin").val()) return Swal.fire("Origin required"), false;
    if (!$("#destination").val()) return Swal.fire("Destination required"), false;
    if (!$("#pcs").val()) return Swal.fire("Pcs required"), false;
    if (!$("#chargeWeight").val()) return Swal.fire("Charge Weight required"), false;
    if (!$("#invoiceNumber").val()) return Swal.fire("Please Enter Invoice Number !"), false;
    if (!$("#emailId").val()) return Swal.fire("Please Enter Email ID !"), false;

    return true;
}


// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#id").val()) || 0;

    let obj = {

        cbid: id,

        bookingOffice: $("#bookingOffice").val(),
        shipperName: $("#shipperName").val(),
        docketType: $("#docketType").val(),

        awb: $("#awb").val(),
        bookingDate: $("#bookingDate").val(),
        origin: $("#origin").val(),
        destination: $("#destination").val(),
        mode: $("#mode").val(),
        product: $("#product").val(),

        pcs: $("#pcs").val(),
        actualWeight: $("#actualWeight").val(),
        volumetric: $("#volumetric").val(),
        volWeight: $("#volWeight").val(),
        chargeWeight: $("#chargeWeight").val(),

        consignorMobile: $("#cMobile").val(),
        consignorGST: $("#cGst").val(),
        consignorName: $("#cName").val(),
        consignorAddress1: $("#cAddress1").val(),
        consignorAddress2: $("#cAddress2").val(),
        consignorCity: $("#cCity").val(),
        consignorPincode: $("#cPincode").val(),
        consignorState: $("#cState").val(),

        consigneeMobile: $("#cnMobile").val(),
        consigneeGST: $("#cnGst").val(),
        consigneeName: $("#cnName").val(),
        consigneeAddress1: $("#cnAddress1").val(),
        consigneeAddress2: $("#cnAddress2").val(),
        consigneeCity: $("#cnCity").val(),
        consigneePincode: $("#cnPincode").val(),
        consigneeState: $("#cnState").val(),

        oda: $("#oda").is(":checked"),

        referenceNo: $("#referenceNo").val(),
        remarks: $("#remarks").val(),

        invoiceNumber: $("#invoiceNumber").val(),
        emailId: $("#emailId").val(),

        paymentMode: $("#paymentMode").val(),
        receivedAmount: $("#receivedAmount").val(),
        tariffAmount: $("#tariffAmount").val(),
        cgst: $("#cgst").val(),
        sgst: $("#sgst").val(),
        igst: $("#igst").val(),
        total: $("#total").val()
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function (res) {
            let cbid = res?.cbid || res?.data?.cbid || id;
            saveItems(cbid);
        }
    });
}


// ================= SAVE ITEMS =================
function saveItems(cbid) {

    let requests = [];

    deletedItems.forEach(x => {
        requests.push($.ajax({
            url: ITEM_API + "/" + x.cbIId,
            type: "DELETE"
        }));
    });

    items.forEach(x => {

        x.cbid = cbid;

        if (x.cbIId && x.cbIId > 0) {
            requests.push($.ajax({
                url: ITEM_API + "/" + x.cbIId,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(x)
            }));
        } else {
            requests.push($.ajax({
                url: ITEM_API,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(x)
            }));
        }
    });

    if (requests.length === 0) {
        Swal.fire("Success", "Saved Successfully", "success");
        modal.hide();
        loadData();
        return;
    }

    $.when.apply($, requests).then(() => {
        Swal.fire("Success", "Saved Successfully", "success");
        modal.hide();
        loadData();
    });
}


// ================= EDIT =================
function edit(id) {

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;

        $("#id").val(x.cbid);

        $("#bookingOffice").val(x.bookingOffice);
        $("#shipperName").val(x.shipperName);
        $("#docketType").val(x.docketType);

        $("#awb").val(x.awb);
        $("#bookingDate").val(formatDateInput(x.bookingDate));
        $("#origin").val(x.origin);
        $("#destination").val(x.destination);

        $("#pcs").val(x.pcs);
        $("#chargeWeight").val(x.chargeWeight);

        $("#invoiceNumber").val(x.invoiceNumber);
        $("#emailId").val(x.emailId);

        $("#tariffAmount").val(x.tariffAmount);
        $("#cgst").val(x.cgst);
        $("#sgst").val(x.sgst);
        $("#igst").val(x.igst);
        $("#total").val(x.total);

        loadItems(id);

        modal.show();
    });
}


// ================= ITEMS =================
function loadItems(cbid) {

    $.get(ITEM_API, function (res) {

        let data = res?.data || res;

        items = data.filter(x => x.cbid == cbid);
        deletedItems = [];

        renderItems();
    });
}

function addItem() {

    if (!$("#invoiceId").val()) return Swal.fire("Invoice ID required");

    let item = {
        cbIId: 0,
        invoiceId: $("#invoiceId").val(),
        invoiceDate: $("#invoiceDate").val(),
        description: $("#description").val(),
        amount: $("#amount").val(),
        ewayBill: $("#ewayBill").val(),
        ewbDate: $("#ewbDate").val()
    };

    items.push(item);
    renderItems();
    clearItemInputs();
}

function removeItem(i) {

    let item = items[i];

    if (item.cbIId && item.cbIId > 0) {
        deletedItems.push(item);
    }

    items.splice(i, 1);
    renderItems();
}

function renderItems() {

    let html = "";

    items.forEach((x, i) => {

        html += `<tr>
            <td>${i + 1}</td>
            <td>${x.invoiceId || ''}</td>
            <td>${formatDate(x.invoiceDate)}</td>
            <td>${x.description || ''}</td>
            <td>${x.amount || ''}</td>
            <td>${x.ewayBill || ''}</td>
            <td>${formatDate(x.ewbDate)}</td>
            <td>
                <button class="btn btn-danger btn-sm btn-delete" data-index="${i}">
                    <i class="bi bi-trash"></i>
                </button>
            </td>
        </tr>`;
    });

    $("#itemTable").html(html);
}

function calculateGST() {

    let t = parseFloat($("#tariffAmount").val()) || 0;

    // ✅ Normalize state values (IMPORTANT FIX)
    let cState = ($("#cState").val() || "").toString().trim().toLowerCase();
    let cnState = ($("#cnState").val() || "").toString().trim().toLowerCase();

    let cgst = 0, sgst = 0, igst = 0;

    // ✅ SAME STATE → CGST + SGST
    if (cState && cnState && cState === cnState) {

        cgst = t * 0.09;
        sgst = t * 0.09;
        igst = 0;

    }
    // ✅ DIFFERENT STATE → IGST
    else if (cState && cnState) {

        igst = t * 0.18;
        cgst = 0;
        sgst = 0;
    }

    $("#cgst").val(cgst.toFixed(2));
    $("#sgst").val(sgst.toFixed(2));
    $("#igst").val(igst.toFixed(2));

    $("#total").val((t + cgst + sgst + igst).toFixed(2));
}

// ================= EXTRA =================
function getRate() {
    let awb = $("#awb").val();
    if (!awb) return Swal.fire("Enter AWB");

    $.get(API + "/GetRate?awb=" + awb, res => {
        $("#tariffAmount").val(res.tariff || 0);
        calculateGST();
    });
}

function sendMail() {
    let email = $("#emailId").val();
    if (!email) return Swal.fire("Please Enter Email ID !");
    $.post(API + "/SendMail", { email: email }, () => Swal.fire("Mail Sent"));
}

function printInvoice() {
    let inv = $("#invoiceNumber").val();
    if (!inv) return Swal.fire("Please Enter Invoice Number !");
    window.open(API + "/Print/" + $("#id").val());
}


// ================= HELPERS =================
function formatDate(date) {
    return date ? new Date(date).toLocaleDateString("en-GB") : '';
}

function formatDateInput(date) {
    return date ? new Date(date).toISOString().split('T')[0] : '';
}

function clearForm() {
    $("input").val('');
    $("select").val('');
    $("#oda").prop("checked", false);
    items = [];
    deletedItems = [];
    renderItems();
}

function clearItemInputs() {
    $("#invoiceId,#invoiceDate,#description,#amount,#ewayBill,#ewbDate").val('');
}