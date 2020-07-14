import angular from 'angular';

angular.module("webbit.services").factory("Alert", $mdToast => {
    const show = (text, cssClass) =>
      $mdToast.show(
        $mdToast.simple()
          .textContent(text)
          .toastClass(`${cssClass}`)
          .hideDelay(2000)
          .position("bottom")
      );

    return {
        success: text => 
            show(text, 'success'),

        error: text => 
          show(text, 'error'),
    };
})