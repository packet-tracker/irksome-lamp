# -*- coding: utf-8 -*-
"""
    locus.blueprint
    ~~~~~~~~~~~~~~~~~~

    locus 어플리케이션에 적용할 blueprint 모듈.

    :copyright: (c) 2015 by nooyahs.
    :license: MIT LICENSE 2.0, see license for more details.
"""


from flask import Blueprint
from locus.locus_logger import Log

locus = Blueprint('locus', __name__,
                     template_folder='../templates', static_folder='../static')

Log.info('static folder : %s' % locus.static_folder)
Log.info('template folder : %s' % locus.template_folder)
