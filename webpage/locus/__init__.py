# -*- coding: utf-8 -*-
"""
    locus
    ~~~~~~~~

    locus 패키지 초기화 모듈.
    locus에 대한 flask 어플리케이션을 생성함.
    config, blueprint, session, DB연결 등을 초기화함.

    :copyright: (c) 2015 by nooyahs.
    :license: MIT LICENSE 2.0, see license for more details.
"""

import os
from flask import Flask, render_template, request, url_for


def print_settings(config):
    print '========================================================'
    print 'SETTINGS for LOCUS APPLICATION'
    print '========================================================'
    for key, value in config:
        print '%s=%s' % (key, value)
    print '========================================================'

''' HTTP Error Code 404와 500은 errorhanlder에 application 레벨에서
    적용되므로 app 객체 생성시 등록해준다.
'''
def not_found(error):
    return render_template('404.html'), 404

def server_error(error):
    err_msg = str(error)
    return render_template('500.html', err_msg=err_msg), 500
    
    
def url_for_other_page(page):
    args = request.view_args.copy()
    args['page'] = page
    return url_for(request.endpoint, **args)

    
def create_app(config_filepath='resource/config.cfg'):
    locus_app = Flask(__name__)

    # 기본 설정은 'LocusConfig' 객체에 정의되있고 운영 환경 또는 기본 설정을 변경을 하려면
    # 실행 환경변수인 LOCUS_SETTING에 변경할 설정을 담고 있는 파일 경로를 설정
    from locus.locus_config import LocusConfig
    locus_app.config.from_object(LocusConfig)
    locus_app.config.from_pyfile(config_filepath, silent=True)
    print_settings(locus_app.config.iteritems())
        
    # 로그 초기화
    from locus.locus_logger import Log
    log_filepath = os.path.join(locus_app.root_path,
                                locus_app.config['LOG_FILE_PATH'])
    Log.init(log_filepath=log_filepath)
    
    # 데이터베이스 처리 
    from locus.database import DBManager
    db_filepath = os.path.join(locus_app.root_path,
                               locus_app.config['DB_FILE_PATH'])
    db_url = locus_app.config['DB_URL'] + db_filepath
    DBManager.init(db_url, eval(locus_app.config['DB_LOG_FLAG']))
    DBManager.init_db()
       
    # 뷰 함수 모듈은 어플리케이션 객체 생성하고 블루프린트 등록전에 
    # 뷰 함수가 있는 모듈을 임포트해야 해당 뷰 함수들을 인식할 수 있음
    from locus.controller import *
    
    from locus.locus_blueprint import locus
    locus_app.register_blueprint(locus)
    
    # SessionInterface 설정.
    # Redis를 이용한 세션 구현은 cache_session.RedisCacheSessionInterface 임포트하고
    # app.session_interface에 RedisCacheSessionInterface를 할당
    from locus.cache_session import SimpleCacheSessionInterface
    locus_app.session_interface = SimpleCacheSessionInterface()
    
    # 공통으로 적용할 HTTP 404과 500 에러 핸들러를 설정
    locus_app.error_handler_spec[None][404] = not_found
    locus_app.error_handler_spec[None][500] = server_error
    
    # 페이징 처리를 위한 템플릿 함수
    locus_app.jinja_env.globals['url_for_other_page'] = \
        url_for_other_page
    
    return locus_app

