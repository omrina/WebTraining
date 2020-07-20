import angular from 'angular';
import controller from './comment.controller';
import template from './comment.html';
import '../add-comment/add-comment.component';
import '../../../components/rating/rating.component';
import './comment.less'

const COMPONENT = 'comment';

angular.module('webbit.controllers')
    .component(COMPONENT, {
        bindings: {
            comment: '<',
            parentCommentId: '<'
        },
        template,
        controller
    });

export default COMPONENT;