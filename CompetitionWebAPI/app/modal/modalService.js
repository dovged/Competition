'use strict';
app.factory('modalService', function () {

    var modalSrviceFactory = {};
    var modals = [];

    var _addModal = new function (modal) {
        modals.push(modal);
    }

    var _removeModal = function(id){
        var modalToRemove = _.findWhere(modals, { id: id });
        modals = _.without(modals, modalToRemove);

    }

    var _openModal = function (id) {
        var modal = _.findWhere(modals, { id: id });
        modal.open();
    }

    var _closeModal = function (id) {
        var modal = _.findWhere(modals, { id: id });
        modal.close();
    }

    modalSrviceFactory.addModal = _addModal;
    modalSrviceFactory.removeModal = _removeModal;
    modalSrviceFactory.openModal = _openModal;
    modalSrviceFactory.closeModal = _closeModal;

    return modalSrviceFactory;

});
