function deleteItem(url) {
    if (confirm("czy na pewno usunąć")) {
        $.ajax({
            type: "POST",
            url: url,
            success: function (result) { 
                location.reload(); 
            }    
        })
    }
}  

